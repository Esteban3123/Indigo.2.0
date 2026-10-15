CREATE TABLE [dbo].[ADCONTIPS] (
    [CODIGOIPS]   CHAR (100)                                                         NOT NULL,
    [DSCRIPIPS]   VARCHAR (50)                                                       NOT NULL,
    [ESTADO]      BIT                                                                NOT NULL,
    [CODIGONIT]   CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)') NULL,
    [DIRECCIPS]   VARCHAR (100)                                                      NULL,
    [TELEFOIPS]   VARCHAR (15)                                                       NULL,
    [CORREOIPS]   VARCHAR (100)                                                      NULL,
    [DEPMUNCOD]   CHAR (5)                                                           NULL,
    [NIVATEIPS]   CHAR (1)                                                           NULL,
    [CLASEIPSS]   CHAR (1)                                                           NULL,
    [NOMREPLEG]   VARCHAR (60)                                                       NULL,
    [IDEREPLEG]   VARCHAR (15)                                                       NULL,
    [CODCENATE]   CHAR (10)                                                          NULL,
    [CODHABILITA] VARCHAR (15)                                                       NOT NULL,
    [DISTANCIA]   NUMERIC (18)                                                       NULL,
    [TIEMESPERA]  NUMERIC (18)                                                       NULL,
    CONSTRAINT [PK_ADCONTIPS] PRIMARY KEY CLUSTERED ([CODIGOIPS] ASC),
    CONSTRAINT [FK_ADINGRESO_ADCONTIPS] FOREIGN KEY ([CODIGOIPS]) REFERENCES [dbo].[ADCONTIPS] ([CODIGOIPS]),
    CONSTRAINT [FK_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [CODHABILITA] UNIQUE NONCLUSTERED ([CODHABILITA] ASC)
);


GO
ALTER TABLE [dbo].[ADCONTIPS] NOCHECK CONSTRAINT [FK_ADINGRESO_ADCONTIPS];


GO
ALTER TABLE [dbo].[ADCONTIPS] NOCHECK CONSTRAINT [FK_CODCENATE];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de espera remitido en h', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TIEMESPERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Distancia en km', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DISTANCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de Habilitación de la Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODHABILITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del respresentante legal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'IDEREPLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Representante legal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NOMREPLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clase  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CLASEIPSS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'NIVATEIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Municipio  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CORREOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Telefono Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'TELEFOIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion  Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DIRECCIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Activo=1;Inactivo:0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'DSCRIPIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Institucion Prestadora de Servicios de Salud ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTIPS', @level2type = N'COLUMN', @level2name = N'CODIGOIPS';

