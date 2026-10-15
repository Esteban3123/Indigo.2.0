CREATE TABLE [dbo].[RCVEHICTRAS] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Codigo]              VARCHAR (3)   NOT NULL,
    [Nombre]              VARCHAR (100) NOT NULL,
    [Estado]              BIT           NOT NULL,
    [Ambulancia]          BIT           NOT NULL,
    [TipoAmbulancia]      INT           NULL,
    [FechaCreacion]       DATETIME      NOT NULL,
    [UsuarioCreacion]     CHAR (20)     NOT NULL,
    [FechaModificacion]   DATETIME      NULL,
    [UsuarioModificacion] CHAR (20)     NULL,
    [IDRCEMPTRANS]        INT           NULL,
    [Placa]               VARCHAR (10)  NULL,
    [MedioTransporte]     INT           NULL,
    CONSTRAINT [PK_RCVEICTRAS] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RCVEHICTRAS_RCEMPTRANS] FOREIGN KEY ([IDRCEMPTRANS]) REFERENCES [dbo].[RCEMPTRANS] ([ID])
);


GO
ALTER TABLE [dbo].[RCVEHICTRAS] NOCHECK CONSTRAINT [FK_RCVEHICTRAS_RCEMPTRANS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Terrestre  2 - Maritimo y/o fluvial  3 - Aéreo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'MedioTransporte';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa del vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Placa';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla RCEMPTRANS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'IDRCEMPTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario que modificó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'UsuarioModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de modificación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'FechaModificacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que creo el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'UsuarioCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'FechaCreacion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el vehiculo es de tipo ambulancia, se debe especificar el tipo de ambulancia ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'TipoAmbulancia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el vehiculo es de tipo ambulancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Ambulancia';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro  1-> Activo  0-> Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Estado';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Nombre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Codigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RCVEHICTRAS', @level2type = N'COLUMN', @level2name = N'Id';

