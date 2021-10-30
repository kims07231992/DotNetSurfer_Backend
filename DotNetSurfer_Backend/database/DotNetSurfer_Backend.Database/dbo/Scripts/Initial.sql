-- CREATE DEFAULT PERMISSION TYPES
INSERT INTO [dbo].[Permissions] ([PermissionId], [PermissionType])
VALUES
	(
		0,
		'Admin'
	),
	(
		1,
		'User'
	);
GO

-- CREATE DEFAULT STATUS TYPES
INSERT INTO [dbo].[Permissions] ([PermissionId], [PermissionType])
VALUES
	(
		0,
		'Requested'
	),
	(
		1,
		'Ongoing'
	),
	(
		2,
		'Pending'
	),
	(
		3,
		'Completed'
	);
GO