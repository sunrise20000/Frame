using Frame.Class;
using Frame.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Frame.Model
{

    public class StationBase : ICommandAction
    {
        private bool bPused=false;
        public bool Enable;
        public string StationName;
        public int StationIndex;
        protected CancellationTokenSource cts =new CancellationTokenSource();
        protected enum STEP
        {
            INIT
        }
        protected Queue<STEP> nStepQueue=new Queue<STEP>();
        protected Task t = null; 
        protected STEP nStep;

        public List<ICommandAction> ReceiverList { get; set; } = new List<ICommandAction>();

        protected virtual bool CheckStationStatusChanged() { return bPused; }
        protected STEP PeekStep() { return nStepQueue.Peek(); }
        protected void PushStep(STEP nStep) { nStepQueue.Enqueue(nStep); }
        protected void PopAndPushStep(STEP nStep) { nStepQueue.Dequeue(); nStepQueue.Enqueue(nStep); }
        protected void PushBatchStep(STEP[] nSteps)
        {
            foreach (var step in nSteps)
                nStepQueue.Enqueue(step);
        }
        protected void PopStep() { nStepQueue.Dequeue(); }
        protected void ClearAllStep() { nStepQueue.Clear(); }
        protected int GetCurStepCount() { return nStepQueue.Count; }
        protected virtual bool UserInit() { return true; }
        protected void ShowInfo(string strMsg)
        {
            SendMessage(new MsgStationInfo() {
                SenderName = GetType().Name,
                Infomation=strMsg,
            });
        }
        public StationBase() {t = new Task(()=>ThreadFunc(this),cts.Token); }
        public bool Start()
        {
            Resume();
            if (!UserInit())
                return false;
            if (t.Status == TaskStatus.Created)
                t.Start();
            else if (t.Status == TaskStatus.Canceled || t.Status == TaskStatus.RanToCompletion)
            {
                cts = new CancellationTokenSource();
                t = new Task(() => ThreadFunc(this), cts.Token);
                t.Start();
            }
            return true;
        }
        public bool Stop()
        {
           cts.Cancel();
           return true;
        }
        public bool Puse()
        {
            bPused = true;
            return true;
        }
        public bool Resume()
        {
            bPused = false;
            return true;
        }
        private static int ThreadFunc(object o) { return (o as StationBase).WorkFlow(); }
        protected virtual int WorkFlow() { return 0; }

        public virtual void SendMessage<T>(T msg, ICommandAction Receive = null) where T : ViewMessageBase
        {
            if (Receive == null)
            {
                foreach (var it in ReceiverList)
                    it.OnRecvMessage(msg);
            }
            else
            {
                Receive.OnRecvMessage(msg);
            }
        }

        public virtual void OnRecvMessage<T>(T msg)
        {
            throw new NotImplementedException();
        }
    }
}
