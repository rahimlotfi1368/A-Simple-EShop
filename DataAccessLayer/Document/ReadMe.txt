Install-Package Microsoft.EntityFrameworkCore -Version 5.0.0-rc.1.20451.13
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 5.0.0-rc.1.20451.13
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 5.0.0-rc.1.20451.13

Entity states and SaveChanges
An entity can be in one of five states as defined by the EntityState enumeration. These states are:

1-Added: the entity is being tracked by the context but does not yet exist in the database
2-Unchanged: the entity is being tracked by the context and exists in the database, and its property values have not changed from the values in the database
3-Modified: the entity is being tracked by the context and exists in the database, and some or all of its property values have been modified
4-Deleted: the entity is being tracked by the context and exists in the database, but has been marked for deletion from the database the next time SaveChanges is called
5-Detached: the entity is not being tracked by the context

SaveChanges does different things for entities in different states:
1-Unchanged entities are not touched by SaveChanges. Updates are not sent to the database for entities in the Unchanged state.
2-Added entities are inserted into the database and then become Unchanged when SaveChanges returns.
3-Modified entities are updated in the database and then become Unchanged when SaveChanges returns.
4-Deleted entities are deleted from the database and are then detached from the context.