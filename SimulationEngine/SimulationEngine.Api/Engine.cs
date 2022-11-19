﻿using SimulationEngine.Api.Events;

namespace SimulationEngine.Api
{
    public static class Engine
    {

        private static readonly PriorityQueue<ManagerEvent, double> listFutureEvents = new();

        public static double Time { get; private set; }

        public static void SimulateOneExecution(Action callback = null)
        {
            if (listFutureEvents.TryDequeue(out var ev, out var priority))
            {
                Time = priority;
                ev.Execute();
                callback?.Invoke();
            }
        }

        public static void Simulate(Action callback = null)
        {
            while (listFutureEvents.TryDequeue(out var ev, out var priority))
            {
                Time = priority;
                ev.Execute();
                callback?.Invoke();
            }
        }

        public static void SimulateUntilDeterminedTime(double time, Action callback = null)
        {
            while (listFutureEvents.TryDequeue(out var ev, out var priority) && priority < time)
            {
                Time = priority;
                ev.Execute();
                callback?.Invoke();
            }
        }

        public static void SimulateForDeterminedTime(double time, Action callback = null)
        {
            var finalTime = time + Time;
            while (listFutureEvents.TryDequeue(out var ev, out var priority) && priority < finalTime)
            {
                Time = priority;
                ev.Execute();
                callback?.Invoke();
            }
        }

        private static void ScheduelEvent(ManagerEvent ev, double timeSelected) => listFutureEvents.Enqueue(ev, timeSelected);

        public static void ScheduleNow(ManagerEvent ev) => ScheduelEvent(ev, Time);        

        public static void AgendarEm(ManagerEvent ev, double timeAdd) => ScheduelEvent(ev, Time + timeAdd);

        public static void AgendarComTempoAbsoluto(ManagerEvent ev, double absoluteTime) => ScheduelEvent(ev, absoluteTime);
    }
}