CREATE TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] (
    [ID]                         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDODONTOCONTROL]            INT       NOT NULL,
    [IDODONTOPLANTRATAMIENTOPAC] INT       NOT NULL,
    [IDODOPARTRA]                INT       NOT NULL,
    [ESTADO]                     INT       NOT NULL,
    [FECHAORDENO]                DATETIME  NULL,
    [PROFESIONALORDENO]          CHAR (20) NULL,
    [FECHAREALIZO]               DATETIME  NULL,
    [PROFESIONALREALIZO]         CHAR (20) NULL,
    [FECHACANCELO]               DATETIME  NULL,
    [PROFESIONALCANCELO]         CHAR (20) NULL,
    CONSTRAINT [PK_ODONTOPLANTRATAMIENTOD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL] FOREIGN KEY ([PROFESIONALORDENO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL1] FOREIGN KEY ([PROFESIONALREALIZO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL2] FOREIGN KEY ([PROFESIONALCANCELO]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOCONTROL] FOREIGN KEY ([IDODONTOCONTROL]) REFERENCES [dbo].[ODONTOCONTROL] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOPLANTRATAMIENTOPAC] FOREIGN KEY ([IDODONTOPLANTRATAMIENTOPAC]) REFERENCES [dbo].[ODONTOPLANTRATAMIENTOPAC] ([ID]),
    CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODOPARTRA] FOREIGN KEY ([IDODOPARTRA]) REFERENCES [dbo].[ODOPARTRA] ([CONSECTRA])
);


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL1];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_INPROFSAL2];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOCONTROL];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODONTOPLANTRATAMIENTOPAC];


GO
ALTER TABLE [dbo].[ODONTOPLANTRATAMIENTOPACD] NOCHECK CONSTRAINT [FK_ODONTOPLANTRATAMIENTOPACD_ODOPARTRA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el profesional quien cancelo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALCANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda le fecha de la cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHACANCELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'profesional que realizó el tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecdha de realización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAREALIZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional ordeno esto se llena si los registros son agregados despues de que ya existen tratamientos al Plan.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'PROFESIONALORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Ordeno el medico, esto se llena si los registros son agregados despues de que ya existen tratamientos al Plan.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'FECHAORDENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Sin realizar (Cuando el paln de tratamiento se encuentra en ejecución y el tratamiento no se ha realizado aún)  2 - Realizado (Cuando se indica que el tratamiendo ya fue realizado)  3 - No realizado (Estado automatico, que se establece cuando un tratamiento no se ha realizado en el momento de indicar que el plan fue Incumplido, los tratamientos en estado sin realizar pasan a No realizados)  4 - Cancelado (Cuando el profesional edita el Plan e indica que el tratamiento no se va a realizar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla ODOPARTRA que es el ID del tratamiento que se le envio al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODOPARTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla ODONTOPLANTRATAMIENTOPAC que es el id del plan de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOPLANTRATAMIENTOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla de control del Odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'IDODONTOCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOPLANTRATAMIENTOPACD', @level2type = N'COLUMN', @level2name = N'ID';

