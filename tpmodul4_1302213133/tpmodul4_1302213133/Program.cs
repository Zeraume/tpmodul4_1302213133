// See https://aka.ms/new-console-template for more information
using System.Transactions;

internal class Program
{
    //Kelurahan
    public enum Kelurahan
    {
        Batununggal,
        Kujangsari,
        Mengger,
        Wates,
        Cijaura,
        Jatisari,
        Margasari,
        Sekejati,
        Kebonwaru,
        Maleer,
        Samoja
    }

    public class KodePos_1302213133
    {
        public static int getKodePos(Kelurahan kelurahan)
        {
            int[] isiKodePos = { 40266, 40287, 40267, 40256, 40287, 40286, 40286, 40286, 40272, 40274, 40273 };
            int inputKodePos = (int)kelurahan;
            return isiKodePos[inputKodePos];
        }
    }

    //Pintu
    public enum DoorState { Terkunci, Terbuka };
    public enum Trigger { KunciPintu, BukaPintu };

    public class DoorMachine_1302213133
    {
        public DoorState CurrentState = DoorState.Terbuka;

        public class Transition
        {
            public DoorState FirstState;
            public DoorState LastState;
            public Trigger trigger;

            public Transition(DoorState FirstState, DoorState LastState, Trigger trigger)
            {
                this.FirstState = FirstState;
                this.LastState = LastState;
                this.trigger = trigger;

            }
        }

        Transition[] transitions = {
            new Transition(DoorState.Terkunci, DoorState.Terkunci, Trigger.KunciPintu),
            new Transition(DoorState.Terbuka, DoorState.Terbuka, Trigger.BukaPintu),
            new Transition(DoorState.Terbuka, DoorState.Terkunci, Trigger.KunciPintu),
            new Transition(DoorState.Terkunci, DoorState.Terbuka, Trigger.BukaPintu),
        };

        private DoorState GetNextState(DoorState FirstState, Trigger trigger)
        {
            DoorState LastState = FirstState;

            for(int i = 0; i < transitions.Length; i++)
            {
                Transition perubahan = transitions[i];

                if(FirstState == perubahan.FirstState && trigger == perubahan.trigger)
                {
                    LastState = perubahan.LastState;
                }
            }

            return LastState;
        }

        public void ActivateTrigger(Trigger trigger)
        {
            CurrentState = GetNextState(CurrentState, trigger);

            Console.WriteLine("State pintu sekarang adalah: " + CurrentState);

            if(CurrentState == DoorState.Terbuka)
            {
                Console.WriteLine("Pintu Terbuka");
            } else if(CurrentState == DoorState.Terkunci)
            {
                Console.WriteLine("Pintu Terkunci");
            }
        }
    }

    public static void Main(string[] args)
    {
        Kelurahan kelurahan = Kelurahan.Samoja;
        int kodePos = KodePos_1302213133.getKodePos(kelurahan);
        Console.WriteLine("Kelurahan: " + kelurahan + "\nKode Pos: " + kodePos);

        Console.WriteLine("----------------------------------------------------");

        DoorMachine_1302213133 pintu = new DoorMachine_1302213133();
        Console.WriteLine(pintu.CurrentState);
        pintu.ActivateTrigger(Trigger.BukaPintu);
        pintu.ActivateTrigger(Trigger.KunciPintu);
        pintu.ActivateTrigger(Trigger.KunciPintu);
    }
}
