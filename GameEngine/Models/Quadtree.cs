using GameEngine.Components;
using GameEngine.Models.ECS.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Models
{
    public class Quadtree
    {
        private readonly List<Entity> _elements = new();

        private readonly int _capacity;
        private readonly int _maxDepth;

        private Quadtree? _topLeft, _topRight, _bottomLeft, _bottomRight;

        public Quadtree(Rectangle bounds)
            : this(bounds, 32, 5)
        { }

        public Quadtree(Rectangle bounds, int bucketCapacity, int maxDepth)
        {
            _capacity = bucketCapacity;
            _maxDepth = maxDepth;

            Bounds = bounds;
        }

        public Rectangle Bounds { get; }

        public int Level { get; init; }

        public bool IsLeaf
            => _topLeft == null || _topRight == null || _bottomLeft == null || _bottomRight == null;

        public void Insert(Entity entity)
        {
            if (_elements.Count >= _capacity)
            {
                Split();
            }

            var collider = entity.GetComponent<ColliderComponent>();

            Quadtree? containingChild = GetContainingChild(collider.Bounds);

            if (containingChild != null)
            {
                containingChild.Insert(entity);
            }
            else
            {
                _elements.Add(entity);
            }
        }
        public bool Remove(Entity entity)
        {
            var collider = entity.GetComponent<ColliderComponent>();

            Quadtree? containingChild = GetContainingChild(collider.Bounds);

            // If no child was returned, then this is the leaf node (or potentially non-leaf node, if the element's boundaries overlap
            // multiple children) containing the element.
            bool removed = containingChild?.Remove(entity) ?? _elements.Remove(entity);

            // If the total descendant element count is less than the bucket capacity, we ensure the node is in a non-split state.
            if (removed && CountElements() <= _capacity)
                Merge();

            return removed;
        }

        public List<Entity> FindCollisions(Entity entity)
        {
            var collider = entity.GetComponent<ColliderComponent>();

            var nodes = new Queue<Quadtree>();
            var collisions = new List<Entity>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();

                if (!collider.Bounds.Intersects(node.Bounds))
                    continue;

                collisions.AddRange(node._elements.Where(e => e.GetComponent<ColliderComponent>().Bounds.Intersects(collider.Bounds)));

                if (!node.IsLeaf)
                {
                    if (collider.Bounds.Intersects(node._topLeft.Bounds))
                        nodes.Enqueue(node._topLeft);

                    if (collider.Bounds.Intersects(node._topRight.Bounds))
                        nodes.Enqueue(node._topRight);

                    if (collider.Bounds.Intersects(node._bottomLeft.Bounds))
                        nodes.Enqueue(node._bottomLeft);

                    if (collider.Bounds.Intersects(node._bottomRight.Bounds))
                        nodes.Enqueue(node._bottomRight);
                }
            }

            return collisions;
        }

        public int CountElements()
        {
            int count = _elements.Count;

            if (!IsLeaf)
            {
                count += _topLeft.CountElements();
                count += _topRight.CountElements();
                count += _bottomLeft.CountElements();
                count += _bottomRight.CountElements();
            }

            return count;
        }

        public IEnumerable<Entity> GetElements()
        {
            var children = new List<Entity>();
            var nodes = new Queue<Quadtree>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var node = nodes.Dequeue();

                if (!node.IsLeaf)
                {
                    nodes.Enqueue(node._topLeft);
                    nodes.Enqueue(node._topRight);
                    nodes.Enqueue(node._bottomLeft);
                    nodes.Enqueue(node._bottomRight);
                }

                children.AddRange(node._elements);
            }

            return children;
        }

        private void Split()
        {   // If we're not a leaf node, then we're already split.
            if (!IsLeaf)
                return;

            // Splitting is only allowed if it doesn't cause us to exceed our maximum depth.
            if (Level + 1 > _maxDepth)
                return;

            _topLeft = CreateChild(Bounds.Location);
            _topRight = CreateChild(new Point(Bounds.Center.X, Bounds.Location.Y));
            _bottomLeft = CreateChild(new Point(Bounds.Location.X, Bounds.Center.Y));
            _bottomRight = CreateChild(new Point(Bounds.Center.X, Bounds.Center.Y));

            var elements = _elements.ToList();

            foreach (var element in elements)
            {
                var collider = element.GetComponent<ColliderComponent>();

                Quadtree? containingChild = GetContainingChild(collider.Bounds);

                // An entity is only moved if it completely fits into a child quadrant
                if (containingChild != null)
                {
                    _elements.Remove(element);

                    containingChild.Insert(element);
                }
            }
        }

        private Quadtree CreateChild(Point location)
            => new(new Rectangle(location.X, location.Y, Bounds.Width / 2, Bounds.Height / 2), _capacity, _maxDepth)
            {
                Level = Level + 1
            };

        private void Merge()
        {   // If we're a leaf node, then there is nothing to merge
            if (IsLeaf)
                return;

            _elements.AddRange(_topLeft._elements);
            _elements.AddRange(_topRight._elements);
            _elements.AddRange(_bottomLeft._elements);
            _elements.AddRange(_bottomRight._elements);

            _topLeft = _topRight = _bottomLeft = _bottomRight = null;
        }

        private Quadtree? GetContainingChild(Rectangle bounds)
        {
            if (IsLeaf)
                return null;

            if (_topLeft.Bounds.Contains(bounds))
                return _topLeft;

            if (_topRight.Bounds.Contains(bounds))
                return _topRight;

            if (_bottomLeft.Bounds.Contains(bounds))
                return _bottomLeft;

            return _bottomRight.Bounds.Contains(bounds) ? _bottomRight : null;
        }
    }
}
