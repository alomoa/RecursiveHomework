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
            Node._idCounter = 0;
            root = new Node("hello");
        }

        [Test]
        public void ShouldAddNode()
        {
            var toAdd = "Goodbye";

            // Act
            NodeFunctions.Add(root, root._id, toAdd);

            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._value, Is.EqualTo(toAdd));

        }

        [Test]
        public void ShouldAddNodeToNestedNode()
        {

            var value1 = "Pizza";
            var value2 = "Vulcan";

            // Act
            NodeFunctions.Add(root, 0, value1);
            NodeFunctions.Add(root, 1, value2); 

            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._sections.Count, Is.EqualTo(1));
            Assert.That(root._sections[0]._sections[0]._value, Is.EqualTo(value2));
        }


        [Test]
        public void ShouldAddNodeMultipleNodes()
        {
            var node1Value = "Pizza";
            var node2Value = "Vulcan";
            var node3Value = "Domino";
            var node4Value = "Stain";



            // Act
            NodeFunctions.Add(root, 0, node1Value);
            NodeFunctions.Add(root, 0, node2Value);
            NodeFunctions.Add(root, 0, node3Value);
            NodeFunctions.Add(root, 0, node4Value);


            //Assert
            Assert.That(root._sections.Count, Is.EqualTo(4));

            Assert.That(root._sections[0]._value, Is.EqualTo(node1Value));
            Assert.That(root._sections[1]._value, Is.EqualTo(node2Value));
            Assert.That(root._sections[2]._value, Is.EqualTo(node3Value));
            Assert.That(root._sections[3]._value, Is.EqualTo(node4Value));
        }


        [Test]
        public void ShouldRemoveFromRootNode()
        {
            //Arrange
            var value = "Pizza";

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, value);
            Assert.That(root._sections.Count, Is.EqualTo(1));

            NodeFunctions.Remove(root, 1);
            Assert.That(root._sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldRemoveFromRootChildrenNode()
        {
            //Arrange
            var nodeValue1 = "Pizza";
            var nodeValue2 = "Vulcan";
            var nodeValue3 = "Domino";
            var nodeValue4 = "Stain";

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, nodeValue1);
            NodeFunctions.Add(root, 0, nodeValue2);
            NodeFunctions.Add(root, 0, nodeValue3);
            NodeFunctions.Add(root, 0, nodeValue4);

            Assert.That(root._sections, Has.Count.EqualTo(4));

            NodeFunctions.Remove(root, 3);
            Assert.That(root._sections, Has.Count.EqualTo(3));
        }

        [Test]
        public void ShouldRemoveFromDoubleNestedNode()
        {
            //Arrange
            var nodeValue1 = "Pizza";
            var nodeValue2 = "Vulcan";

            //Act & Assert & Act & Assert
            NodeFunctions.Add(root, 0, nodeValue1);
            NodeFunctions.Add(root, 1, nodeValue2);


            NodeFunctions.RemoveTheBoringWay(root, 1);
            Assert.That(root._sections.Count, Is.EqualTo(0));
        }

        [Test]
        public void ShouldNotRemoveFromRootNode() {
            //Arrange
            var nodeValue = "Peanuts";

            //Act
            NodeFunctions.Add(root, root._id, nodeValue);
            NodeFunctions.RemoveTheBoringWay(root, 2);

            //Assert
            Assert.That(root._sections, Has.Count.EqualTo(1));
        }

        [Test]
        public void ShouldNotRemoveFromSingleNestedNode()
        {
            //Arrange
            var node1 = new Node("Pizza");
            var node2 = new Node("Vulcan");
            var node3 = new Node("Domino");
            var node4 = new Node("Stain");

            node1._sections = new List<Node>() {node2, node3, node4};
            root._sections = new List<Node>() { node1 };

            //Act
            NodeFunctions.RemoveTheBoringWay(root, 20);

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
            var node1 = new Node("Port");
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
            var node1 = new Node("Port");
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
            var node1 = new Node("Pizza");
            var node2 = new Node("Vulcan");
            var node3 = new Node("Domino");
            var node4 = new Node("Stain");
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
            var node1 = new Node("Pizza");
            var node2 = new Node("Vulcan");
            var node3 = new Node("Domino");
            var node4 = new Node("Stain");
            root._sections = new List<Node>() { node1, node2, node3, node4 };

            //Act
            var result = NodeFunctions.FindChildNode(0, 5, root._sections);

            //Assert
            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public void FindNode_DeepNode()
        {
            //Arrange
            for(var i = 0; i < 100; i++)
            {
                NodeFunctions.Add(root, i, i.ToString());
            }

            //Act
            var result = NodeFunctions.FindNode(root, 99);

            //Assert
            Assert.That(result._value, Is.EqualTo("98"));
        }
    }
}