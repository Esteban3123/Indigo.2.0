CREATE TABLE [dbo].[INAREASER] (
    [ARSCODIGO] CHAR (10)     NOT NULL,
    [ARSDESCRI] VARCHAR (100) NOT NULL,
    [CODCENCOS] CHAR (14)     NOT NULL,
    [CODCUEING] CHAR (15)     NOT NULL,
    [CODCUSUMP] CHAR (15)     NOT NULL,
    [MANAREURG] BIT           NOT NULL,
    [ARPRESATE] CHAR (1)      NOT NULL,
    [CODCUEDES] CHAR (15)     NULL,
    [CODCUINGP] CHAR (15)     NULL,
    [CODCUEHON] CHAR (15)     NULL,
    [TERMEDING] CHAR (1)      NULL,
    [CODBODEGA] VARCHAR (20)  NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [CENATEINV] CHAR (10)     NULL,
    [TERGASMED] BIT           NOT NULL,
    [ESTAREASE] BIT           NOT NULL,
    [CCINGTRA]  VARCHAR (16)  NULL,
    CONSTRAINT [PK_INAREASER] PRIMARY KEY CLUSTERED ([ARSCODIGO] ASC),
    CONSTRAINT [FK_INAREASER_IHBODEGAS] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_INAREASER_INCENTCOS] FOREIGN KEY ([CODCENCOS]) REFERENCES [dbo].[INCENTCOS] ([CODCENCOS])
);


GO
ALTER TABLE [dbo].[INAREASER] NOCHECK CONSTRAINT [FK_INAREASER_IHBODEGAS];


GO
ALTER TABLE [dbo].[INAREASER] NOCHECK CONSTRAINT [FK_INAREASER_INCENTCOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Información NIFF Cx Ingreso tramite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CCINGTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Area de Servicio 1:Activo;0:Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ESTAREASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero del medico en cuenta de ingreso a entidad (0=NO;1=SI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERGASMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CENATEINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Bodegas de Inventarios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tercero del Medico en Cuenta de Ingresos a Entidad 0:No;1:Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'TERMEDING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Cuenta Honorarios Medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEHON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cuenta Contable Para Ingreso de Pacientes Particulares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUINGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta Contable de Descuentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area donde se Presta la Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARPRESATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja Area de Urgencias 1:Si;2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'MANAREURG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de Suministros a Pacientes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUSUMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cuenta de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCUEING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Centros de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Area de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Area de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INAREASER', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';

