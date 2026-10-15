CREATE TABLE [dbo].[INCONTRAC] (
    [CONCODIGO] CHAR (6)      NOT NULL,
    [CONDESCRI] VARCHAR (100) NOT NULL,
    [CODENTIDA] CHAR (9)      NULL,
    [CODENTADM] CHAR (5)      NULL,
    [CODNITTER] CHAR (15)     NULL,
    [CONCONTAC] VARCHAR (150) NULL,
    [NUMCONTRA] VARCHAR (15)  NULL,
    [RESEVAPRE] VARCHAR (10)  NOT NULL,
    [CONIMPUTA] VARCHAR (15)  NOT NULL,
    [CONTVALOR] MONEY         NOT NULL,
    [FECINICON] DATETIME      NOT NULL,
    [FECFINCON] DATETIME      NOT NULL,
    [OBSERVACI] VARCHAR (500) NULL,
    [OBSERVCON] VARCHAR (500) NULL,
    [CONESTADO] CHAR (1)      NOT NULL,
    [MODOFACT]  CHAR (1)      NOT NULL,
    [CONIMPRIS] CHAR (1)      NOT NULL,
    [TIPLEGALI] BIT           NOT NULL,
    [FECLEGALI] DATETIME      NOT NULL,
    [CONDIRECC] VARCHAR (50)  NULL,
    [MONFACCON] MONEY         NULL,
    [TOMONFACT] CHAR (1)      NOT NULL,
    [CONEMAIL]  VARCHAR (50)  NULL,
    CONSTRAINT [PK_INCONTRAC] PRIMARY KEY CLUSTERED ([CONCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Monto Facturado de 1: Total del Servicios;2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TOMONFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Monto Facturado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MONFACCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Legalizacion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Legalizado 1:Legalizado;2: No Legalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imprimir servicios de 1=IPS;2=EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo Facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MODOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Fin del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECFINCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECINICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONTVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imputacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reserva Presupuestal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODNITTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCODIGO';

