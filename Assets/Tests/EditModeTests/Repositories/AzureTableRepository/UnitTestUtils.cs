// Copyright (c) Petr Havel 2023.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;

namespace HoloLens4Labs.Tests
{
    public static class UnityTestUtils
    {

        public static T RunAsyncMethodSync<T>(Func<Task<T>> asyncFunc)
        {
            return Task.Run(async () => await asyncFunc()).GetAwaiter().GetResult();
        }
        public static void RunAsyncMethodSync(Func<Task> asyncFunc)
        {
            Task.Run(async () => await asyncFunc()).GetAwaiter().GetResult();
        }
    }
}