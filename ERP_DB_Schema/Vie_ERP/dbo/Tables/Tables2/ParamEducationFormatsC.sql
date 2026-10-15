CREATE TABLE [dbo].[ParamEducationFormatsC] (
    [Id]                            INT           IDENTITY (1, 1) NOT NULL,
    [Code]                          VARCHAR (3)   NOT NULL,
    [Type]                          BIT           NOT NULL,
    [Obligatory]                    BIT           NULL,
    [DashboardMedico]               BIT           NULL,
    [DashboardEspecialista]         BIT           NULL,
    [DashboardAtencionFarmaceutica] BIT           NULL,
    [DashboardNursing]              BIT           NULL,
    [DashboardTerapia]              BIT           NULL,
    [DashboardServicioApoyo]        BIT           NULL,
    [Triage]                        BIT           NULL,
    [Description]                   VARCHAR (300) NOT NULL,
    [MinAge]                        INT           NOT NULL,
    [UnitMinAge]                    INT           NOT NULL,
    [MaxAge]                        INT           NOT NULL,
    [UnitMaxAge]                    INT           NOT NULL,
    [Sex]                           INT           NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  CHAR (20)     NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              CHAR (20)     NULL,
    [ModificationDate]              DATETIME      NULL,
    CONSTRAINT [PK_ParamEducationFormatsC] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario de modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado. Activo o Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Masculino  2 - Femenino  3 - Ambos  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Sex';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Años  2 - Meses   3 - Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMaxAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad máxima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MaxAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Años  2 - Meses   3 - Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'UnitMinAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad mínima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'MinAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del formato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad Triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Triage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardServicioApoyo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardServicioApoyo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardTerapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardTerapia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardEnfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardNursing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardAtencionFarmaceuta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardAtencionFarmaceutica';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardEspecialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardEspecialista';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Visibilidad DashboardMedico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'DashboardMedico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el tipo es encuesta este campo indica si es obligatoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Obligatory';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tipo     1 = Formato de educación  0 = Encuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el Codigo formatos educación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ParamEducationFormatsC', @level2type = N'COLUMN', @level2name = N'Id';

