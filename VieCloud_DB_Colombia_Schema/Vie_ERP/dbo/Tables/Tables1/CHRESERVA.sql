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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHRESERVA].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHRESERVA].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHRESERVA].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHRESERVA].[IPPRINOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[CHRESERVA].[IPSEGNOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');

GO
CREATE NONCLUSTERED INDEX [IDX_CHRESERVA_ESTADORES_FECINIEST]
    ON [dbo].[CHRESERVA]([ESTADORES] ASC, [FECINIEST] ASC)
    INCLUDE([CODICAMAS], [FECFINEST], [IPCODPACI], [IPPRIAPEL], [IPSEGAPEL], [IPPRINOMB], [IPSEGNOMB]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la asignación de la reserva de cama (DATETIME). Marca cuándo el profesional asigna la cama al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realiza la asignación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza la asignación de la reserva de cama (FK INPROFSAL.CODPROSAL). Identifica quién asignó la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que realiza la asignación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AssigmentReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la anulación de la reserva de cama (DATETIME). Registra cuándo se cancela la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realiza la anulación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realiza la anulación de la reserva de cama (FK INPROFSAL.CODPROSAL). Identifica quién canceló la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que realiza la anulación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'AnulationReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que confirma la reserva de cama (FK INPROFSAL.CODPROSAL). Valida y autoriza la disponibilidad de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que realiza la confirmación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realiza la confirmación de la reserva de cama (DATETIME). Documenta cuándo se validó la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se realiza la confirmación de la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ConfirmReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que crea la reserva de cama (FK INPROFSAL.CODPROSAL). Identifica quién originó la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que realiza la reserva de la cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se crea la reserva de cama (DATETIME). Marca el momento inicial de la solicitud de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en la cual se crea la reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ReservationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo numérico (NUMERIC 18) para registro de auditoría. Rastrea cambios y acceso a la reserva para cumplimiento normativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para el registro de auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la reserva de cama (TINYINT): 1=Sin Confirmar, 2=Confirmada, 3=Anulada, 4=Asignada/Finalizada. Indica el ciclo de vida de la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ESTADORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Reserva: 
1: Sin Confirmar  
2: Confirmada  
3: Anulada
4 Reserva Asignada / Finalizada
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ESTADORES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'ESTADORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad (aseguradora, EPS, institución) a la que pertenece el paciente (CHAR 9). Vincula la reserva a la responsable financiera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (CHAR 20, PII enmascarado). Componente adicional del nombre para identificación completa del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (CHAR 20, PII enmascarado). Nombre principal para identificación del paciente en la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (CHAR 20, PII enmascarado). Segundo componente del apellido para identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (CHAR 20, PII enmascarado). Apellido principal para identificación del paciente en la reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente equivalente a cédula, número de identificación o documento de identidad (VARCHAR 25, PII enmascarado). Identificador único del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final de la estancia hospitalaria (DATETIME). Marca cuándo se prevé el alta o término de la ocupación de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECFINEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECFINEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial de la estancia hospitalaria (DATETIME). Marca cuándo inicia la ocupación esperada de la cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECINIEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECINIEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación de la reserva de cama (DATETIME). Registro del momento en que se genera la solicitud de cama.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECRESERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECRESERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'FECRESERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la cama hospitalaria (INT, FK CHCAMASHO.CODICAMAS). Identifica la cama específica que se reserva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la reserva de cama (CHAR 10, PK). Identificador correlativo de la transacción (ej: 00000008).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CONRESERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Reserva  Nota: Identificador - 00000008', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CONRESERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA', @level2type = N'COLUMN', @level2name = N'CONRESERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reservas de camas hospitalarias. Registra las solicitudes de reserva de cama para un paciente, incluyendo fechas de inicio y fin de estancia, estado de la reserva y los profesionales que intervienen en cada etapa (creación, confirmación, anulación y asignación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHRESERVA';
