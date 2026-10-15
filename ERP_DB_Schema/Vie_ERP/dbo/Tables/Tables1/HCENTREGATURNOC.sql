CREATE TABLE [dbo].[HCENTREGATURNOC] (
    [ID]            INT       IDENTITY (1, 1) NOT NULL,
    [CODCENATE]     CHAR (10) NOT NULL,
    [UFUCODIGO]     CHAR (10) NOT NULL,
    [JORNADA]       INT       NOT NULL,
    [DASHBOARD]     INT       NOT NULL,
    [FECHAREGISTRO] DATETIME  NOT NULL,
    CONSTRAINT [PK_HCENTREGATURNO_1] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se registra el Entrega de Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dasboard Origen en el que se entrega el turno    0 : DashboardMedico  1 : DashboardEnfermeria  2 : DashboardAcademico  3 : DashboardTerapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'DASHBOARD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jornada en la que se entrega el turno   0 : Mañana  1 : Tarde  2 : Noche    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'JORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCENTREGATURNOC', @level2type = N'COLUMN', @level2name = N'ID';

