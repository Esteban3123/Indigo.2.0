CREATE TABLE [Security].[PersonInt] (
    [Id]                 INT             NOT NULL,
    [Identification]     VARCHAR (15)    NOT NULL,
    [IdentificationType] SMALLINT        NOT NULL,
    [FirstName]          VARCHAR (50)    NOT NULL,
    [SecondName]         VARCHAR (50)    NULL,
    [FirstLastName]      VARCHAR (50)    NOT NULL,
    [SecondLastName]     VARCHAR (50)    NULL,
    [Fullname]           VARCHAR (250)   NOT NULL,
    [BirthDay]           DATETIME        NULL,
    [Fingerprint]        VARBINARY (MAX) NULL,
    [Gender]             SMALLINT        NOT NULL,
    [State]              BIT             NOT NULL
);

GO
CREATE NONCLUSTERED INDEX IX_PersonInt_Id_Performance
ON Security.PersonInt (Id)
INCLUDE (Fullname);

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Personas internas del sistema (empleados, profesionales, usuarios internos): guarda los datos de identificación, nombre, fecha de nacimiento, huella dactilar y estado de cada persona registrada en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único de la persona en el sistema.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de documento de identidad de la persona (cédula, identificación, documento).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Identification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identidad (cédula de ciudadanía, pasaporte, tarjeta de identidad, etc.).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'IdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'FirstName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre de la persona (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'SecondName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'FirstLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido de la persona (puede estar vacío).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'SecondLastName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la persona, concatenación de nombres y apellidos.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Fullname';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Fullname';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento de la persona.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'BirthDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'BirthDay';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar digitalizada de la persona, usada para identificación biométrica.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Fingerprint';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Género o sexo de la persona (masculino, femenino, otro).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'Gender';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo o inactivo de la persona en el sistema (activo = 1, inactivo = 0).', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'PersonInt', @level2type = N'COLUMN', @level2name = N'State';
