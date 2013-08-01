using System;
using System.Collections.Generic;

namespace WikiTools
{
    public class ReadAheadEnumerator<T> : IEnumerator<T>
    {
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
                return _itemAheadIsValid;
            }
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
