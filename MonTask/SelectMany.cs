using System;
using System.Threading.Tasks;

namespace MonTask {
    public static partial class Extensions {
        public static async Task SelectMany(this Task task, Func<Task> fn) {
            await task.ConfigureAwait(false);
            await fn().ConfigureAwait(false);
        }

        public static async Task<TSource> SelectMany<TSource>(this Task task, Func<Task> fn,
            Func<TSource> resultSelector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            await task.ConfigureAwait(false);
            await fn().ConfigureAwait(false);
            return resultSelector();
        }

        public static async Task<TResult> SelectMany<TResult>(this Task task,
            Func<Task<TResult>> selector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            await task.ConfigureAwait(false);
            return await selector().ConfigureAwait(false);
        }

        public static async Task<TResult> SelectMany<TTask, TResult>(this Task task,
            Func<Task<TTask>> selector, Func<TTask, TResult> resultSelector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            await task.ConfigureAwait(false);
            return resultSelector(await selector().ConfigureAwait(false));
        }

        public static async Task SelectMany<TSource>(this Task<TSource> task, Func<TSource, Task> fn) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (fn == null) throw new ArgumentNullException(nameof(fn));
            await fn(await task.ConfigureAwait(false)).ConfigureAwait(false);
        }

        public static async Task<TResult> SelectMany<TSource, TTask, TResult>(this Task<TSource> task,
            Func<TSource, Task<TTask>> selector, Func<TSource, TTask, TResult> resultSelector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            var source = await task.ConfigureAwait(false);
            return resultSelector(source, await selector(source).ConfigureAwait(false));
        }

        public static async Task<TResult> SelectMany<TSource, TResult>(this Task<TSource> task,
            Func<TSource, Task> selector, Func<TSource, TResult> resultSelector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (resultSelector == null) throw new ArgumentNullException(nameof(resultSelector));
            var source = await task.ConfigureAwait(false);
            await selector(source).ConfigureAwait(false);
            return resultSelector(source);
        }

        public static async Task<TResult> SelectMany<TSource, TResult>(this Task<TSource> task,
            Func<TSource, Task<TResult>> selector) {
            if (task == null) throw new ArgumentNullException(nameof(task));
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            return await selector(await task.ConfigureAwait(false)).ConfigureAwait(false);
        }
    }
}