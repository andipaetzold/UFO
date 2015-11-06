namespace UFO.Domain
{
    public abstract class DatabaseObject
    {
        #region Fields

        private int? id;

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                if (id == null)
                {
                    throw new NoIdException("No Id is set, because the object hasn't been added to the database, yet.");
                }

                return id.Value;
            }
            set { id = value; }
        }

        #endregion
    }
}
