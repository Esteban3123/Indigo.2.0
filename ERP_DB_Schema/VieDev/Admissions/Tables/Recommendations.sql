CREATE TABLE [Admissions].[Recommendations] (
    [CODCENATE]      VARCHAR (10)  NOT NULL,
    [NOMCENATE]      VARCHAR (100) NULL,
    [DASHACADEMIC]   BIT           NULL,
    [DASHMEDIC]      BIT           NULL,
    [DASHSPECIALIST] BIT           NULL,
    [DASHENFERM]     BIT           NULL,
    [SUPPORTSERVICE] BIT           NULL,
    [DASHTHERAPY]    BIT           NULL,
    CONSTRAINT [PK__Recommen__23952C637ACE1CED] PRIMARY KEY CLUSTERED ([CODCENATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard de terapia (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHTHERAPY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio de soporte (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'SUPPORTSERVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard enfermeria (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHENFERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard especialista (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHSPECIALIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard médico (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHMEDIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dashboard acadenico (true or false)', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'DASHACADEMIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'NOMCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código centro de atención', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'Recommendations', @level2type = N'COLUMN', @level2name = N'CODCENATE';

