using Frame.Class;
using Frame.Definations;
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
        private string OldMsg = "";
        private bool bPused { get; set; } = false;
        public bool Enable { get; set; } = false;
        public string StationName { get; set; }
        public int StationIndex { get; set; } = 0;
        protected CancellationTokenSource cts =new CancellationTokenSource();
        protected Queue<int> nStepQueue=new Queue<int>();
        protected Task t = null; 
        public List<ICommandAction> ReceiverList { get; set; } = new List<ICommandAction>();
        protected virtual bool CheckStationStatusChanged() { return bPused; }
        protected T PeekStep<T>() where T:struct
        {
            int i = nStepQueue.Peek();
            if (Enum.TryParse(i.ToString(), out T result))
                return result;
            else
                return default(T);

        }
        protected void PushStep<T>(T nStep) where T:struct { nStepQueue.Enqueue(nStep.GetHashCode()); }
        protected void PopAndPushStep<T>(T nStep) where T:struct { nStepQueue.Dequeue(); nStepQueue.Enqueue(nStep.GetHashCode()); }
        protected void PushBatchStep<T>(params T[] nSteps) where T:struct
        {
            foreach (var step in nSteps)
                nStepQueue.Enqueue(step.GetHashCode());
        }
        protected void PopStep() { nStepQueue.Dequeue(); }
        protected void ClearAllStep() { nStepQueue.Clear(); }
        protected int GetCurStepCount() { return nStepQueue.Count; }
        protected virtual bool UserInit() { return true; }
        protected void ShowInfo(string strMsg,bool IsCanRepeat=false)
        {
            if(IsCanRepeat || OldMsg != strMsg)
            { 
                SendMessage(new MsgStationInfo()
                {
                    SenderName = GetType().Name,
                    Infomation = strMsg,
                });
                OldMsg = strMsg;
            }
        }
        public StationBase() {
            t = new Task(()=>ThreadFunc(this),cts.Token);
            StationName = GetType().Name;
        }
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
        public bool Wait(int timeOut=5000)
        {
            if (t.Status == TaskStatus.Running)
                return t.Wait(timeOut);
            else
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
