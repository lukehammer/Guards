﻿using System;
using System.Diagnostics;

#if NETSTANDARD
using System.Reflection;
#endif

namespace Guards
{
#if !DEBUG
    [DebuggerStepThrough]
#endif
    public static partial class Guard
    {
        /// <summary>
        /// Checks if the given <paramref name="type"/> is an interface type.
        /// </summary>
        /// <exception cref="ArgumentException">The <paramref name="type" /> parameter is not an interface type.</exception>
        public static void ArgumentMustBeInterface([ValidatedNotNull]Type type)
        {
            CheckIfTypeIsInterface(type, false, ExceptionMessages.ArgumentMustBeInterface);
        }

        /// <summary>
        /// Checks if the given <paramref name="type"/> is not an interface type.
        /// </summary>
        /// <exception cref="ArgumentException">The <paramref name="type" /> parameter is an interface type.</exception>
        public static void ArgumentMustNotBeInterface([ValidatedNotNull]Type type)
        {
            CheckIfTypeIsInterface(type, true, ExceptionMessages.ArgumentMustNotBeInterface);
        }

        private static void CheckIfTypeIsInterface(Type type, bool throwIfItIsAnInterface, string exceptionMessage)
        {
            ArgumentNotNull(type, nameof(type));

#if NETSTANDARD
            if (type.GetTypeInfo().IsInterface == throwIfItIsAnInterface)
#else
            if (type.IsInterface == throwIfItIsAnInterface)
#endif
            {
                throw new ArgumentException(exceptionMessage, type.Name);
            }
        }
    }
}