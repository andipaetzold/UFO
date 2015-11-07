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
                    throw new DatabaseIdException("No Id is set, because the object hasn't been added to the database.");
                }

                return id.Value;
            }
        }

        #endregion

        public void DeletedFromDatabase()
        {
            if (id != null)
            {
                throw new DatabaseIdException("This object hasn't been added to a database.");
            }

            id = null;
        }

        public void InsertedInDatabase(int insertId)
        {
            if (id != null)
            {
                throw new DatabaseIdException("This object has already been added to a database.");
            }

            id = insertId;
        }
    }
}
