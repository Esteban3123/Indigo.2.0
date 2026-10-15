CREATE TABLE [dbo].[CHRESERVA] (
    [CONRESERV]                        CHAR (10)    NOT NULL,
    [CODICAMAS]                        INT          NOT NULL,
    [FECRESERV]                        DATETIME     NOT NULL,
    [FECINIEST]                        DATETIME     NOT NULL,
    [FECFINEST]                        DATETIME     NOT NULL,
    [IPCODPACI]                        VARCHAR (25) NOT NULL,
    [IPPRIAPEL]                        CHAR (20)    NOT NULL,
    [IPSEGAPEL]                        CHAR (20)    NOT NULL,
    [IPPRINOMB]                        CHAR (20)    NOT NULL,
    [IPSEGNOMB]                        CHAR (20)    NOT NULL,
    [CODENTIDA]                        CHAR (9)     NOT NULL,
    [ESTADORES]                        TINYINT      NOT NULL,
    [INDAUDFOR]                        NUMERIC (18) NOT NULL,
    [ReservationDate]                  DATETIME     NULL,
    [ReservationProfessional]          CHAR (20)    NULL,
    [ConfirmReservationDate]           DATETIME     NULL,
    [ConfirmReservationProfessional]   CHAR (20)    NULL,
    [AnulationReservationProfessional] CHAR (20)    NULL,
    [AnulationReservationDate]         DATETIME     NULL,
    [AssigmentReservationProfessional] CHAR (20)    NULL,
    [AssigmentReservationDate]         DATETIME     NULL,
    CONSTRAINT [PK_CHRESERVA] PRIMARY KEY CLUSTERED ([CONRESERV] ASC),
    CONSTRAINT [FK_CHRESERVA_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHRESERVA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_CHRESERVA_INPROFSALANULA] FOREIGN KEY ([AnulationReservationProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHRESERVA_INPROFSALASIGNA] FOREIGN KEY ([AssigmentReservationProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHRESERVA_INPROFSALCONFIRMA] FOREIGN KEY ([ConfirmReservationProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_CHRESERVA_INPROFSALRESERVA] FOREIGN KEY ([ReservationProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_INPACIENT];


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_INPROFSALANULA];


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_INPROFSALASIGNA];


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_INPROFSALCONFIRMA];


GO
ALTER TABLE [dbo].[CHRESERVA] NOCHECK CONSTRAINT [FK_CHRESERVA_INPROFSALRESERVA];


GO
CREATE NONCLUSTERED INDEX [IDX_CHRESERVA_ESTADORES_FECINIEST]
    ON [dbo].[CHRESERVA]([ESTADORES] ASC, [FECINIEST] ASC)
    INCLUDE([CODICAMAS], [FECFINEST], [IPCODPACI], [IPPRIAPEL], [IPSEGAPEL], [IPPRINOMB], [IPSEGNOMB]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realiza la asignación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que realiza la asignación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realiza la anulación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que realiza la anulación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que realiza la confirmación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se realiza la confirmación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que realiza la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en la cual se crea la reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para el registro de auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la Reserva: 
1: Sin Confirmar  
2: Confirmada  
3: Anulada
4 Reserva Asignada / Finalizada
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ESTADORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inicial de la Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECRESERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Reserva  Nota: Identificador - 00000008', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CONRESERV';

