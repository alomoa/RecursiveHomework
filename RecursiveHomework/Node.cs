namespace RecursiveHomework
{
    public class Node
    {
        public static int _idCounter = 0;
        public int _id;
        public string _value;
        public List<Node> _sections;

        public Node(string value)
        {
            _id = _idCounter;
            _idCounter++;
            _value = value;
            _sections = new List<Node>();
        }
    }

    public class NodeFunctions
    {
        public static void Add(Node node, int targetId, string value)
        {
            if (node._id == targetId)
            {
                var toAdd = new Node(value);
                node._sections.Add(toAdd);
            }
            else
            {
                foreach (var section in node._sections)
                {
                    Add(section, targetId, value);
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

        public static void RemoveTheBoringWay(Node node, int targetId)
        {
            if (node._id == targetId)
            {
                node = null;
            }
            else
            {
                var found = node._sections.Where(x => x._id == targetId);
                if (found.Count() > 0)
                {
                    node._sections.Remove(found.First());
                    return;
                }
                foreach(var childNode in node._sections)
                {
                    
                    RemoveTheBoringWay(childNode, targetId);
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

    }

}
