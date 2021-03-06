﻿namespace Microsoft.AspNetCore.Authentication.ActiveDirectory
{
    using Extensions.WebEncoders;
    using Microsoft.AspNet.Authentication;
    using Microsoft.AspNet.Builder;
    using Microsoft.Extensions.Logging;
    using System;
    public class ActiveDirectoryMiddleware : AuthenticationMiddleware<ActiveDirectoryOptions>
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ActiveDirectoryOptions _options;

        /// <summary>
        /// Creates a new instance of the ActiveDirectoryMiddleware.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="options">The Active Directory configuration options.</param>
        /// <param name="loggerFactory">An <see cref="ILoggerFactory"/> instance used to create loggers.</param>
        public ActiveDirectoryMiddleware(
            RequestDelegate next,
            ActiveDirectoryOptions options,
            ILoggerFactory loggerFactory,
            IUrlEncoder encoder)
            : base(next, options, loggerFactory, encoder)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (encoder == null)
            {
                throw new ArgumentNullException(nameof(encoder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _next = next;
            _logger = loggerFactory.CreateLogger<ActiveDirectoryMiddleware>();
        }

        protected override AuthenticationHandler<ActiveDirectoryOptions> CreateHandler()
        {
            return new NtlmAuthenticationHandler();
        }
    }
}
