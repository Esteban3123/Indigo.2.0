CREATE TABLE [dbo].[INPACIENTPYP] (
    [ID]             INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [TIPOCOTIZANTE]  VARCHAR (100)                                                                    NULL,
    [IDCOTIZANTE]    VARCHAR (100)                                                                    NULL,
    [EMPCOTIZANTE]   VARCHAR (100)                                                                    NULL,
    [EPS]            VARCHAR (100)                                                                    NULL,
    [FECHAAFILIA]    DATE                                                                             NULL,
    [SEMCOTIZADAS]   VARCHAR (100)                                                                    NULL,
    [NUMCARNET]      VARCHAR (100)                                                                    NULL,
    [TIPODOCUMEN]    VARCHAR (100)                                                                    NULL,
    [FECEXPEDI]      DATE                                                                             NULL,
    [PRIMERNOM]      VARCHAR (100)                                                                    NULL,
    [SEGNOMBRE]      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')    NULL,
    [PRIMAPELLI]     VARCHAR (100)                                                                    NULL,
    [SEGAPELLI]      VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)') NULL,
    [PARENTECOTIZA]  VARCHAR (100)                                                                    NULL,
    [FECHNACI]       DATE                                                                             NULL,
    [SEXO]           VARCHAR (100)                                                                    NULL,
    [ESTADOCIVIL]    VARCHAR (100)                                                                    NULL,
    [DISOACIDAD]     VARCHAR (100)                                                                    NULL,
    [GRUPOPOBLA]     VARCHAR (100)                                                                    NULL,
    [GRUPOETNICO]    VARCHAR (100)                                                                    NULL,
    [DIRECCION]      VARCHAR (100) MASKED WITH (FUNCTION = 'default()')                               NULL,
    [TEL1]           VARCHAR (100)                                                                    NULL,
    [TEL2]           VARCHAR (100)                                                                    NULL,
    [EMAIL]          VARCHAR (100)                                                                    NULL,
    [URBANDO]        VARCHAR (100)                                                                    NULL,
    [MUNICIPIO]      VARCHAR (100)                                                                    NULL,
    [DEPARTAMEN]     VARCHAR (100)                                                                    NULL,
    [NUMFICHSISBEN]  VARCHAR (100)                                                                    NULL,
    [NIVELSISBEN]    VARCHAR (100)                                                                    NULL,
    [TIPOAFILIACION] VARCHAR (100)                                                                    NULL,
    [ESTADOAFILIA]   VARCHAR (100)                                                                    NULL,
    [IPSASIGNADA]    VARCHAR (100)                                                                    NULL,
    [SEXUALACTIVA]   VARCHAR (100)                                                                    NULL,
    [GRAVIDEZ]       VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_INPACIENTPYP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_INPACIENTPYP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[INPACIENTPYP] NOCHECK CONSTRAINT [FK_INPACIENTPYP_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la gravidez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRAVIDEZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda  la sexual activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXUALACTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la ip asignada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IPSASIGNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOAFILIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOAFILIACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el nivel del sisben', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NIVELSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número de la ficha del sisben ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMFICHSISBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DEPARTAMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'se guarda el municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'MUNICIPIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda urbano', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'URBANDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el teledono 2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el telefono 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TEL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOETNICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el grupo poblacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'GRUPOPOBLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Discapacitado (1. Si, 2. No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'DISOACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el estado civil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parentesco con el cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PARENTECOTIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMAPELLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEGNOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'PRIMERNOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la fecha de expedición', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECEXPEDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el tipo de documento de identidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPODOCUMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el número del carnet', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'NUMCARNET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas cotizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'SEMCOTIZADAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de afiliación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'FECHAAFILIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la entidad prestadora de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Empleado cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'EMPCOTIZANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el Id cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'IDCOTIZANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N' Se guarda el tipo de cotizante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENTPYP', @level2type = N'COLUMN', @level2name = N'TIPOCOTIZANTE';

