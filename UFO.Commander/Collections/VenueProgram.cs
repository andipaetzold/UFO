namespace UFO.Commander.Collections
{
    using System.Collections.Generic;
    using UFO.Domain;

    public class VenueProgram
    {
        #region Properties

        public static Artist NullArtist { get; } = new Artist("", null, null, null, null, null, false);
        public Dictionary<int, Performance> Times { get; } = new Dictionary<int, Performance>();
        public Venue Venue { get; set; }

        #endregion
    }
}
