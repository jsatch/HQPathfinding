using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.jsatch.games.collections
{
    public class QueueElement<T>
    {
        public T Elem { private set; get; }
        public int Value { private set; get; }
        public QueueElement<T> NextElement = null;
        public QueueElement<T> PreviousElement = null;

        public QueueElement(T elem, int value)
        {
            Elem = elem;
            Value = value;
        }
    }

    public class PriorityQueue<T>
    {
        public int Length { get; private set; }
        private QueueElement<T> mTopElement = null;
        private QueueElement<T> mBottomElement = null;

        public PriorityQueue()
        {
            Length = 0;
        }

        public void Enqueue(T elementValue, int priority)
        {
            var element = new QueueElement<T>(elementValue, priority);
            if (mTopElement == null)
            {
                mTopElement = element;
                mBottomElement = element;
            }
            else
            {
                var pElem = mTopElement;
                QueueElement<T> pElemBefore = null;
                while (pElem != null)
                {
                    if (priority > pElem.Value)
                    {
                        if (pElem.PreviousElement == null)
                        {
                            // Adding at the beginning
                            mTopElement = element;
                            element.NextElement = pElem;
                            pElem.PreviousElement = element;
                        }
                        else
                        {
                            var temp = pElem.PreviousElement;
                            element.NextElement = pElem;
                            pElem.PreviousElement = element;
                            temp.NextElement = element;
                            element.PreviousElement = temp;
                        }
                        Length++;
                        return;
                    }
                    pElemBefore = pElem;
                    pElem = pElem.NextElement;
                }

                pElemBefore.NextElement = element;
                element.PreviousElement = pElemBefore;
                mBottomElement = element;

            }
            Length++;
        }

        public T GetTop()
        {
            return mTopElement.Elem;
        }

        public T GetBottom()
        {
            return mBottomElement.Elem;
        }

        public QueueElement<T> GetTopQueueElement()
        {
            return mTopElement;
        }

        public QueueElement<T> GetBottomQueueElement()
        {
            return mBottomElement;
        }

        public T DequeueTop()
        {
            if (mTopElement == null) return default(T);
            var elemValue = mTopElement.Elem;
            mTopElement = mTopElement.NextElement;
            Length--;
            return elemValue;
        }

        public T DequeueBottom()
        {
            if (mTopElement == null) return default(T);

            var elemValue = mBottomElement.Elem;
            if (Length > 1)
            {
                mBottomElement = mBottomElement.PreviousElement;
                mBottomElement.NextElement = null;
            }
            else
            {
                mTopElement = null;
                mBottomElement = null;
            }
            Length--;
            return elemValue;
        }

        public QueueElement<T> DequeueTopQueueElement()
        {
            if (mTopElement == null) return null;
            var element = mTopElement;
            mTopElement = mTopElement.NextElement;
            Length--;
            return element;
        }

        public QueueElement<T> DequeueBottomQueueElement()
        {
            if (mTopElement == null) return null;
            var element = mBottomElement;
            if (Length == 1)
            {
                mTopElement = null;
                mBottomElement = null;
            }
            else
            {
                mBottomElement = mBottomElement.PreviousElement;
                mBottomElement.NextElement = null;
            }
            Length--;
            return element;
        }

        public override string ToString()
        {
            string resp = "";

            var pElement = mTopElement;
            while (pElement != null)
            {
                resp += $"{pElement.Elem} : ({pElement.Value})\n";
                pElement = pElement.NextElement;
            }

            return resp;
        }
    }
}
