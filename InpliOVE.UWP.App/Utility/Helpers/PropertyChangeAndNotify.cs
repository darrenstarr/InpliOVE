﻿namespace InpliOVE.UWP.App.Utility.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public static class PropertyChangeAndNotify
    {
        public static bool ChangeAndNotify<T>
            (
                this PropertyChangedEventHandler handler,
                ref T field,
                T value,
                Expression<Func<T>> memberExpression,
                bool force=false
            )
        {
            if (memberExpression == null)
                throw new ArgumentNullException("memberExpression");

            var body = memberExpression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("Lambda must return a property.");

            if (!force && EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;

            if (body.Expression is ConstantExpression vmExpression)
            {
                LambdaExpression lambda = Expression.Lambda(vmExpression);
                Delegate vmFunc = lambda.Compile();
                object sender = vmFunc.DynamicInvoke();

                handler?.Invoke(sender, new PropertyChangedEventArgs(body.Member.Name));
            }

            return true;
        }
    }
}
