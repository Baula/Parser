using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WikiTools;

namespace WikiTools.Tests
{
    [TestClass]
    public class ReadAheadEnumeratorTests
    {
        [TestMethod]
        public void ApiDemo()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");

            bool thereIsAnItem;
            Action moveNextAction = () => { enumerator.MoveNext(); };
            char currentItem;
            Action getCurrentAction = () => { var dummy = enumerator.Current; };
            Action getItemAheadAction = () => { var dummy = enumerator.ItemAhead; };

            // Enumerator has not been moved
            getCurrentAction.ShouldThrow<InvalidOperationException>()
                            .WithMessage("Move next has never been called.");

            // Move to the 1st item
            thereIsAnItem = enumerator.MoveNext();

            thereIsAnItem.Should().BeTrue();
            currentItem = enumerator.Current;
            currentItem.Should().Be('a');
            enumerator.CanReadAhead.Should().BeTrue();
            enumerator.ItemAhead.Should().Be('b');

            // Move to the 2nd item
            thereIsAnItem = enumerator.MoveNext();

            thereIsAnItem.Should().BeTrue();
            enumerator.Current.Should().Be('b');
            enumerator.CanReadAhead.Should().BeFalse();
            getItemAheadAction.ShouldThrow<InvalidOperationException>()
                              .WithMessage("The enumerator reached the last item of the sequence and thus cannot read ahead.");
            
            // Move beyond the end
            thereIsAnItem = enumerator.MoveNext();

            thereIsAnItem.Should().BeFalse();
            getCurrentAction.ShouldThrow<InvalidOperationException>()
                            .WithMessage("The enumerator reached the end of the sequence.");
            moveNextAction.ShouldThrow<InvalidOperationException>()
                          .WithMessage("The enumerator reached the end of the sequence.");
        }

        [TestMethod]
        public void Current_OnNewEnumeratorWithinSequence_ThrowsAProperException()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");

            new Action(() => { var dummy = enumerator.Current; }).ShouldThrow<InvalidOperationException>()
                                                                 .WithMessage("Move next has never been called.");
        }

        [TestMethod]
        public void CanReadAhead_OnNewEnumeratorWithinSequence_ReturnsTrue()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");

            enumerator.CanReadAhead.Should().BeTrue();
        }

        [TestMethod]
        public void MoveNext_OnNewEnumeratorWithNonEmptySequence_ReturnsTrue()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");

            var actual = enumerator.MoveNext();

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Current_AfterTheFirstMoveNext_ReturnsTheFirstItem()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();

            var actual = enumerator.Current;

            actual.Should().Be('a');
        }

        [TestMethod]
        public void CanReadAhead_OnNextToLastItem_ReturnsTrue()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();

            enumerator.CanReadAhead.Should().BeTrue();
        }

        [TestMethod]
        public void CanReadAhead_OnLastItem_ReturnsFalse()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();
            enumerator.MoveNext();

            enumerator.CanReadAhead.Should().BeFalse();
        }

        [TestMethod]
        public void ItemAhead_OnLastItem_ThrowsProperException()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();
            enumerator.MoveNext();

            new Action(() => { var dummy = enumerator.ItemAhead; }).ShouldThrow<InvalidOperationException>()
                                                                   .WithMessage("The enumerator reached the last item of the sequence and thus cannot read ahead.");
        }

        [TestMethod]
        public void MoveNext_ToTheLastItem_ReturnsTrue()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();

            // Act
            var actual = enumerator.MoveNext();

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanReadAhead_OnMovedEnumeratorWithinSequence_ReturnsTrue()
        {
            var enumerator = new ReadAheadEnumerator<char>("ab");
            enumerator.MoveNext();

            enumerator.CanReadAhead.Should().BeTrue();
        }

        [TestMethod]
        public void FullScenarioWithEmptySequence()
        {
            var enumerator = new ReadAheadEnumerator<char>("");

            Action moveNextAction = () => { enumerator.MoveNext(); };
            Action getCurrentAction = () => { var dummy = enumerator.Current; };
            Action getItemAheadAction = () => { var dummy = enumerator.ItemAhead; };

            var thereIsAnItem = enumerator.MoveNext();
            thereIsAnItem.Should().BeFalse();
            enumerator.CanReadAhead.Should().BeFalse();
            getCurrentAction.ShouldThrow<InvalidOperationException>();
            getItemAheadAction.ShouldThrow<InvalidOperationException>();
            moveNextAction.ShouldThrow<InvalidOperationException>();
        }
    }
}
