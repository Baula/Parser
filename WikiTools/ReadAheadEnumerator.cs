using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiTools
{
    public class ReadAheadEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<T> _sequence;
        private bool _hasBeenCalled = false;
        private readonly IEnumerator<T> _enumerator;
        private bool _itemAheadIsValid;
        private T _current;
        private T _itemAhead;
        private bool _currentIsValid;

        public ReadAheadEnumerator(IEnumerable<T> sequence)
        {
            _enumerator = sequence.GetEnumerator();
            _itemAheadIsValid = _enumerator.MoveNext();
            if (_itemAheadIsValid)
                _itemAhead = _enumerator.Current;
        }

        public bool MoveNext()
        {
            if (!_currentIsValid && _hasBeenCalled)
                throw new InvalidOperationException("The enumerator reached the end of the sequence.");
            _current = _itemAhead;
            _currentIsValid = _itemAheadIsValid;
            _itemAheadIsValid = _enumerator.MoveNext();
            if (_itemAheadIsValid)
                _itemAhead = _enumerator.Current;
            _hasBeenCalled = true;
            return _currentIsValid;
        }

        public T Current
        {
            get
            {
                if (!_hasBeenCalled)
                    throw new InvalidOperationException("Move next has never been called.");
                if (!_currentIsValid)
                    throw new InvalidOperationException("The enumerator reached the end of the sequence.");
                return _current;
            }
        }

        public T ItemAhead
        {
            get
            {
                if (!_itemAheadIsValid)
                    throw new InvalidOperationException("The enumerator reached the last item of the sequence and thus cannot read ahead.");
                return _itemAhead;
            }
        }

        public bool CanReadAhead
        {
            get
            {
                //var aheadEnum = _sequence.GetEnumerator();
                //for (var i = 1; i <= itemCount; i++)
                //{
                //    var couldMove = aheadEnum.MoveNext();
                //    if (!couldMove)
                //        return false;
                //}
                return _itemAheadIsValid;
                //return itemCount <= _sequence.Count();
            }
        }

        public T ReadAhead(int itemCount)
        {
            return _sequence.Last();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
