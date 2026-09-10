CREATE VIEW [Common].[ViewLatestEmailByPerson]
AS
	SELECT e.IdPerson PersonId, MAX(e.Id) EmailId
	FROM Common.Email e WITH (NOLOCK)
	GROUP BY e.IdPerson
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra el correo electrónico más reciente registrado para cada persona en el sistema, seleccionando el último email ingresado según su identificador. Consolida la tabla de emails agrupando por persona y tomando el registro de mayor ID, lo que permite obtener un único correo vigente por individuo. Es útil para procesos de comunicación, notificaciones y validación de contacto, evitando duplicados cuando una persona tiene múltiples correos registrados.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewLatestEmailByPerson';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewLatestEmailByPerson';
GO
