using System;
using Python.Runtime;

namespace YTMDotNet.YTMAPI {
    // Py.GIL() wrapper to get YTmusicAPI library at the same time
    internal class PyYTMAPI : IDisposable {
        public const string YTMAPIModuleName = "ytmusicapi";

        private readonly Py.GILState state;
        private bool isDisposed;

        public readonly dynamic API;

        internal PyYTMAPI() {
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

        ~PyYTMAPI() {
            // copy exception from Py.GILState.~GILState, since you can't call finalizers in C#...
            throw new InvalidOperationException("GIL must always be released, and it must be released from the same thread that acquired it.");
        }
    }
}

#if nocompile
    // Py.GILState source
    public static GILState GIL() {
        return new GILState();
    }

    public class GILState : IDisposable {
        private readonly IntPtr state;
        private bool isDisposed;

        internal GILState() {
            state = PythonEngine.AcquireLock();
        }

        public virtual void Dispose() {
            if (this.isDisposed) 
                return;

            PythonEngine.ReleaseLock(state);
            GC.SuppressFinalize(this);
            this.isDisposed = true;
        }

        ~GILState() {
            throw new InvalidOperationException("GIL must always be released, and it must be released from the same thread that acquired it.");
        }
    }
#endif
