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

