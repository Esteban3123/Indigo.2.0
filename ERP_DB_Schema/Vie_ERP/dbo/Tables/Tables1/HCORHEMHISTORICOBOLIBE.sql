CREATE TABLE [dbo].[HCORHEMHISTORICOBOLIBE] (
    [ID]           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORHEMBOL] INT            NOT NULL,
    [HCORHEMCOID]  INT            NOT NULL,
    [COMSAMID]     INT            NOT NULL,
    [FECRESERVA]   DATETIME       NOT NULL,
    [BACRESPRES]   CHAR (20)      NOT NULL,
    [REAPRUECRU]   BIT            NOT NULL,
    [NUMBOLSA]     VARCHAR (20)   NOT NULL,
    [SELLOCALIDAD] VARCHAR (20)   NOT NULL,
    [GRUPOBOL]     TINYINT        NOT NULL,
    [FECHAEXPIRA]  DATETIME       NOT NULL,
    [PROFLIBERO]   CHAR (20)      NOT NULL,
    [MOTLIBRESID]  INT            NOT NULL,
    [MOTLIBRESDES] VARCHAR (5000) NOT NULL,
    [ASPECFISICO]  TINYINT        NULL,
    [TEMPENTREG]   INT            NULL,
    [AUTENTREGA]   BIT            NULL,
    CONSTRAINT [PK_HCORHEMHISTORICOBOLIBE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si autoriza entrega:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'AUTENTREGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temperatura de la bolsa en la entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'TEMPENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspecto fisico de la unidad (bolsa):   1=Adecuado   2=Inadecuado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ASPECFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del Motivo de Liberación Hemocomponentes Reservados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'MOTLIBRESID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesional que pasa la bolsa a estado liberado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'PROFLIBERO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECHAEXPIRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de bolsa:  Grupo de bolsa:  1 - A+  2 - A-  3 - B+  4 - B-  5 - AB+  6 - AB-  7 - O+  8 - O-', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'GRUPOBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sello de calidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'SELLOCALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de bolsa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'NUMBOLSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si realiza prueba cruzada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'REAPRUECRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bacteriologo Responsable de Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'BACRESPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'FECRESERVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del componente sanguíneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'COMSAMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que relaciona el ID de la tabla HCORHEMCO (Cabecera solictud de hemocomponentes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'HCORHEMCOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID HCORHEMBOL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'IDHCORHEMBOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMHISTORICOBOLIBE', @level2type = N'COLUMN', @level2name = N'ID';

