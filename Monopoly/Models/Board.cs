using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;

namespace Models {
    public class Board : IObservable<Command>, IUpdatable
    {
        //private List<Robot> worldObjects = new List<Robot>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();
        
        public Board() {

        }


        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer)) {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }
            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c) {
            for(int i = 0; i < this.observers.Count; i++) {
                this.observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs) {
            //foreach(robot m3d in worldObjects) {
            //    obs.OnNext(new UpdateModel3DCommand(m3d));
            //}
        }

        public bool Update(int tick)
        {
            //for(int i = 0; i < worldObjects.Count; i++) {

            //    if(u is IUpdatable) {
            //        bool needsCommand = ((IUpdatable)u).Update(tick);

            //        if(needsCommand) {
            //            SendCommandToObservers(new UpdateModel3DCommand(u));
            //        }
            //    }
            //}

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose() 
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}