CREATE TABLE [dbo].[HCLISTACC] (
    [CODCONSEC]    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMENCENF]    VARCHAR (200) NOT NULL,
    [GENEROAPLI]   INT           NULL,
    [UNIEDADMI]    INT           NULL,
    [EDADMINIM]    INT           NULL,
    [UNIEDADMA]    INT           NULL,
    [EDADMAXIM]    INT           NULL,
    [ESTADOENC]    INT           NULL,
    [INAPLICA]     INT           NULL,
    [APMEDENF]     INT           NULL,
    [TIPO]         INT           CONSTRAINT [DF_HCLISTACC_TIPO] DEFAULT ((1)) NOT NULL,
    [APLICADIAGNO] BIT           NULL,
    CONSTRAINT [PK_HCLISTCHE] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que indica si la lista de chequeo aplica "Para todos" o "Ninguno" diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APLICADIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1: Seguridad paciente  2: Cirugía  3: Atención domiciliaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Medico 2 - Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'APMEDENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1- Aplica para todas las Unidades Funcionales  0- Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'INAPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1 activo -  2 inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'ESTADOENC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Maxima en la que aplica  la lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Maxima: 1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima en la que aplica la lista de Chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'EDADMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida para Edad Minima:  1: Años    2: Meses    3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo Aplicacion Encuesta 1 Masculino  - 2 femenino - 3  Aplica para Ambas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'GENEROAPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la lista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'NOMENCENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumarico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';

