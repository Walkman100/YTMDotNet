using System;
using Python.Runtime;

namespace YTMDotNet.YTMAPI {
    static class PyYTMAPI {
        public readonly static string YTMAPIModuleName = "ytmusicapi";
        internal static YTMAPIDisposable Get() =>
            new YTMAPIDisposable();

        // Py.GIL() wrapper to get YTmusicAPI library at the same time
        internal class YTMAPIDisposable : IDisposable {
            private readonly Py.GILState state;
            public readonly dynamic API;
            private bool isDisposed;

            internal YTMAPIDisposable() {
                state = Py.GIL();

                dynamic YTMusicAPI = Py.Import(YTMAPIModuleName);
                API = YTMusicAPI.YTMusic(Helpers.Settings.HeadersPath);
            }

            public virtual void Dispose() {
                if (this.isDisposed)
                    return;

                state.Dispose();
                GC.SuppressFinalize(this);
                this.isDisposed = true;
            }

            ~YTMAPIDisposable() {
                // copy exception from Py.GILState.~GILState, since you can't call finalizers in C#...
                throw new InvalidOperationException("GIL must always be released, and it must be released from the same thread that acquired it.");
            }
        }
    }
}
