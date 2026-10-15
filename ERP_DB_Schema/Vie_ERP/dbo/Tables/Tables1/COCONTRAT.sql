CREATE TABLE [dbo].[COCONTRAT] (
    [CODCONTRA] CHAR (6)        NOT NULL,
    [CODENTIDA] CHAR (9)        NOT NULL,
    [CODENTADM] CHAR (9)        NULL,
    [CODNITTER] CHAR (15)       NULL,
    [CONCONTAC] VARCHAR (150)   NULL,
    [RESEVAPRE] VARCHAR (10)    NULL,
    [CONIMPUTA] VARCHAR (15)    NULL,
    [DESCONTRA] CHAR (90)       NOT NULL,
    [NUMCONTRA] CHAR (15)       NOT NULL,
    [CONFECLEG] DATETIME        NOT NULL,
    [CONTVALOR] NUMERIC (18, 2) NOT NULL,
    [CONFECINI] DATETIME        NOT NULL,
    [CONFECFIN] DATETIME        NOT NULL,
    [CONOBSERV] TEXT            NULL,
    [OBSERVCON] TEXT            NULL,
    [CONESTADO] INT             NULL,
    [MODOFACT]  CHAR (1)        NULL,
    [CONIMPRIS] CHAR (1)        NULL,
    [TIPLEGALI] BIT             NULL,
    [CONDIRECC] VARCHAR (50)    NULL,
    [MONFACCON] MONEY           NULL,
    [TOMONFACT] CHAR (1)        NULL,
    [CONEMAIL]  VARCHAR (50)    NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    [REGIMEN]   VARCHAR (50)    NULL,
    CONSTRAINT [PK_INCONTRAT] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC),
    CONSTRAINT [FK_COCONTRAT_INENTADM] FOREIGN KEY ([CODENTADM]) REFERENCES [dbo].[INENTADM] ([CODENTADM])
);


GO
ALTER TABLE [dbo].[COCONTRAT] NOCHECK CONSTRAINT [FK_COCONTRAT_INENTADM];


GO
CREATE NONCLUSTERED INDEX [_dta_index_COCONTRAT_6_656773447__K2_8]
    ON [dbo].[COCONTRAT]([CODENTIDA] ASC)
    INCLUDE([DESCONTRA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Régimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'REGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo Electronico del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Monto Facturado de 1: Total del Servicios;2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TOMONFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Monto Facturado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MONFACCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Legalizado 1:Legalizado;2: No Legalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imprime servicio de 1:IPS;2:EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo Facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MODOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Contrato:  1: Activo  2: Suspendido  3: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'OBSERVCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion General para el Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONTVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Legalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Contrato Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'DESCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Imputacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reserva Presupuestal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODNITTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Entidad EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';

