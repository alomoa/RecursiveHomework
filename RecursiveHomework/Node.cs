namespace RecursiveHomework
{
    public class Node
    {
        public int _id;
        public string _value;
        public List<Node> _sections;

        public Node(int id, string value)
        {
            _id = id;
            _value = value;
            _sections = new List<Node>();
        }
    }

    public class NodeFunctions
    {
        public static void Add(Node node, int targetId, Node toAdd)
        {
            if (node._id == targetId && NodeIdDoesNotExist(node, toAdd))
            {
                node._sections.Add(toAdd);
            }
            else
            {
                foreach (var section in node._sections)
                {
                    Add(section, targetId, toAdd);
                }
            }
        }

        public static void Update(Node node, int targetId, string value)
        {
            if (node._id == targetId)
            {
                node._value = value;
            }
            else
            {
                foreach (var section in node._sections)
                {
                    Update(section, targetId, value);
                }
            }
        }

        public static void Remove(Node node, int targetId)
        {
            if (node._id == targetId)
            {
                node = null;
            }
            else
            {
                var foundNode = FindChildNode(0, targetId, node._sections);
                if (foundNode != null)
                {
                    node._sections.Remove(foundNode);
                    return;
                }

                foreach (var section in node._sections)
                {
                    Remove(section, targetId);
                }
            }
        }

        public static Node FindChildNode(int index, int idToFind, List<Node> children)
        {
            if (index == children.Count)
            {
                return null;
            }

            if (children[index]._id == idToFind)
            {
                return children[index];
            }
            else
            {
                return FindChildNode(index + 1, idToFind, children);
            }
        }

        public static bool NodeIdDoesNotExist(Node node, Node toCheck)
        {
            if (node._id == toCheck._id)
            {
                return false;
            }
            else
            {
                var foundNode = FindChildNode(0, toCheck._id, node._sections);
                if (foundNode != null)
                {
                    return false;
                }

                foreach (var section in node._sections)
                {
                    return NodeIdDoesNotExist(section, toCheck);
                }
                return true;
            }
        }
    }

}
