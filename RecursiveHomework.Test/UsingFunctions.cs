using NUnit.Framework;
using RecursiveHomework;

namespace RecursibeHomework.Test
{
    public class UsingFunctions
    {
        Node root;

        [SetUp]
        public void Setup()
        {
            root = new Node(0, "hello");
        }

        [Test]
        public void ShouldAddNode()
        {
            var node1 = new Node(1, "Goodbye");

            // Act
            NodeFunctions.Add(root, root._id, node1);

            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._value, Is.EqualTo(node1._value));

        }

        [Test]
        public void ShouldAddNodeToNestedNode()
        {
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");

            // Act
            NodeFunctions.Add(root, 0, node1);
            NodeFunctions.Add(root, 1, node2); 

            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._sections[0]._value, Is.EqualTo("Vulcan"));
        }


        [Test]
        public void ShouldAddNodeMultipleNodes()
        {
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");
            var node3 = new Node(3, "Domino");
            var node4 = new Node(4, "Stain");



            // Act
            NodeFunctions.Add(root, 0, node1);
            NodeFunctions.Add(root, 0, node2);
            NodeFunctions.Add(root, 0, node3);
            NodeFunctions.Add(root, 0, node4);


            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(4));

            Assert.That(root._sections[0], Is.EqualTo(node1));
            Assert.That(root._sections[1], Is.EqualTo(node2));
            Assert.That(root._sections[2], Is.EqualTo(node3));
            Assert.That(root._sections[3], Is.EqualTo(node4));
        }

        [Test]
        public void ShouldNotAddToNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");

            //Act & Assert
            NodeFunctions.Add(node1, 1, node1);
            Assert.That(node1._sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void NodeIdDoesNotExist_ShouldReturnFalse()
        {
            Assert.That(NodeFunctions.NodeIdDoesNotExist(root, root), Is.False);
        }


        [Test]
        public void NodeIdDoesNotExist_ShouldReturnTrue()
        {
            //Arrange
            var nodeToAdd = new Node(2, "Value");

            //Act
            Assert.That(NodeFunctions.NodeIdDoesNotExist(root, nodeToAdd), Is.True);
        }

        [Test]
        public void ShouldRemoveFromRootNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, node1);
            Assert.That(root._sections.Count, Is.EqualTo(1));

            NodeFunctions.Remove(root, node1._id);
            Assert.That(root._sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldRemoveFromRootChildrenNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");
            var node3 = new Node(3, "Domino");
            var node4 = new Node(4, "Stain");

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, node1);
            NodeFunctions.Add(root, 0, node2);
            NodeFunctions.Add(root, 0, node3);
            NodeFunctions.Add(root, 0, node4);

            Assert.That(root._sections.Count, Is.EqualTo(4));

            NodeFunctions.Remove(root, node3._id);
            Assert.That(root._sections.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldRemoveFromDoubleNestedNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, node1);
            NodeFunctions.Add(root, node1._id, node2);


            NodeFunctions.Remove(node1, node2._id);
            Assert.That(node2._sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotRemoveFromRootNode() {
            //Arrange
            var node = new Node(1, "Peanuts");

            //Act
            NodeFunctions.Add(root, root._id, node);
            NodeFunctions.Remove(root, 2);

            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(1));
        }

        [Test]
        public void ShouldNotRemoveFromSingleNestedNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");
            var node3 = new Node(3, "Domino");
            var node4 = new Node(4, "Stain");

            node1._sections = new List<Node>() {node2, node3, node4};
            root._sections = new List<Node>() { node1 };

            //Act
            NodeFunctions.Remove(root, 20);

            //Assert
            Assert.That(node1._sections.Count, Is.EqualTo(3));
        }

        [Test]
        public void ShouldUpdateRoot()
        {
            //Act
            NodeFunctions.Update(root, 0, "Hey");

            //Assert
            Assert.That(root._value, Is.EqualTo("Hey"));
        }

        [Test]
        public void ShouldUpdateRootChild()
        {
            //Arrange
            var node1 = new Node(1, "Port");
            root._sections = new List<Node>() { node1 };


            //Act
            NodeFunctions.Update(root, node1._id, "Hey");

            //Assert
            Assert.That(node1._value, Is.EqualTo("Hey"));
        }

        [Test]
        public void ShouldNotUpdate()
        {
            //Arrange
            var node1 = new Node(1, "Port");
            root._sections = new List<Node>() { node1 };


            //Act
            NodeFunctions.Update(root, 2, "Hey");

            //Assert
            Assert.That(root._value, Is.EqualTo("hello"));
            Assert.That(node1._value, Is.EqualTo("Port"));
        }

        [Test]
        public void FindNode_FindsNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");
            var node3 = new Node(3, "Domino");
            var node4 = new Node(4, "Stain");
            root._sections = new List<Node>() { node1, node2, node3, node4 };

            //Act
            var result = NodeFunctions.FindChildNode(0, node3._id, root._sections);

            //Assert
            Assert.That(result, Is.EqualTo(node3));
        }

        [Test]
        public void FindNode_DoesNotNode()
        {
            //Arrange
            var node1 = new Node(1, "Pizza");
            var node2 = new Node(2, "Vulcan");
            var node3 = new Node(3, "Domino");
            var node4 = new Node(4, "Stain");
            root._sections = new List<Node>() { node1, node2, node3, node4 };

            //Act
            var result = NodeFunctions.FindChildNode(0, 5, root._sections);

            //Assert
            Assert.That(result, Is.EqualTo(null));
        }
    }
}