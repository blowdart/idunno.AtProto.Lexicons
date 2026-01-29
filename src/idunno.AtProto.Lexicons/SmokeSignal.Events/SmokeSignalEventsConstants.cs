// Copyright (c) Barry Dorrans. All rights reserved.
// Licensed under the MIT License.

namespace idunno.AtProto.Lexicons.SmokeSignal.Events
{
    /// <summary>
    /// A static class to hold constant information.
    /// </summary>
    public static class SmokeSignalEventsConstants
    {
        /// <summary>
        /// The collection for smoke signal records.
        /// </summary>
        public static Nsid Collection => new("events.smokesignal");
    }
}
