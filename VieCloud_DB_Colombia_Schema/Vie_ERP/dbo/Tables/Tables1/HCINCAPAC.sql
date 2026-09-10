CREATE TABLE [dbo].[HCINCAPAC] (
    [CODCONSEC]                    NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]                    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECINIINC]                    DATETIME                                                                         NOT NULL,
    [FECFININC]                    DATETIME                                                                         NOT NULL,
    [NUMDIAINC]                    INT                                                                              NOT NULL,
    [CODDIAGNO]                    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [ICAUSAING]                    INT                                                                              NOT NULL,
    [TIPOINCAP]                    INT                                                                              NULL,
    [FECREGIST]                    DATETIME                                                                         NOT NULL,
    [FECACCLAB]                    DATETIME                                                                         NULL,
    [ESPRORROG]                    BIT                                                                              NULL,
    [CONSECANT]                    NUMERIC (18)                                                                     NULL,
    [NUMINGRES]                    CHAR (10)                                                                        NULL,
    [OBSERVACI]                    CHAR (500)                                                                       NULL,
    [CODJUSINCA]                   CHAR (4)                                                                         NULL,
    [OBSERVAJUSINCA]               CHAR (500)                                                                       NULL,
    [NUMEFOLIO]                    NCHAR (10)                                                                       NULL,
    [DisabilityClass]              INT                                                                              NULL,
    [GroupServices]                INT                                                                              NULL,
    [IdAdmissionModalities]        INT                                                                              NULL,
    [RetroactiveDisability]        BIT                                                                              NULL,
    [CODESPECI]                    CHAR (3)                                                                         NULL,
    [CauseOfRetroactivity]         INT                                                                              NULL,
    [PresumedOrigin]               INT                                                                              NULL,
    [NameOfParentOrCaregiver]      VARCHAR (100)                                                                    NULL,
    [GestationalAge]               NUMERIC (3, 1)                                                                   NULL,
    [MultiplePregnancyGestational] INT                                                                              NULL,
    [FlexibleParentingResponses]   VARCHAR (MAX)                                                                    NULL,
    [State]                        INT                                                                              NULL,
    [CancellationProfessionalId]   CHAR (20)                                                                        NULL,
    [CancellationReasonCodeId]     CHAR (4)                                                                         NULL,
    [CancellationObservation]      VARCHAR (100)                                                                    NULL,
    [CancellationDate]             DATETIME                                                                         NULL,
    [ModificationProfessionalId]   CHAR (20)                                                                        NULL,
    [ModificationReasonId]         CHAR (4)                                                                         NULL,
    [ModificationObservation]      VARCHAR (100)                                                                    NULL,
    [ModificationDate]             DATETIME                                                                         NULL,
    CONSTRAINT [PK_HCINCAPAC] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC),
    CONSTRAINT [FK_CancellationReasonCodeId] FOREIGN KEY ([CancellationReasonCodeId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCINCAPAC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINCAPAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINCAPAC_CODJUSINCA] FOREIGN KEY ([CODJUSINCA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCINCAPAC_HCINCAPAC] FOREIGN KEY ([CONSECANT]) REFERENCES [dbo].[HCINCAPAC] ([CODCONSEC]),
    CONSTRAINT [FK_HCINCAPAC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCINCAPAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINCAPAC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINCAPAC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_IdModificationReason] FOREIGN KEY ([ModificationReasonId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_Incapacidad_Ingreso] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ModificationProfessionalId] FOREIGN KEY ([ModificationProfessionalId]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_PROFESIONAL_ID] FOREIGN KEY ([CancellationProfessionalId]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINCAPAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINCAPAC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINCAPAC].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPROFSAL];

GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPROFSAL];


GO

GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPROFSAL];



GO
CREATE NONCLUSTERED INDEX [IX_HCINCAPAC_IPCODPACI_NUMINGRES]
    ON [dbo].[HCINCAPAC]([IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE TRIGGER [dbo].[Tgr_ActualizaEspecialidadNULL_HCINCAPAC] 
   ON  dbo.HCINCAPAC 
   AFTER INSERT
AS 
BEGIN
	
if exists(select 1 from inserted where CODESPECI is null )begin
		update hc set CODESPECI = prof.CODESPEC1
		from HCINCAPAC hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.CODCONSEC in (select CODCONSEC from inserted)
end

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora en que se modificó (editó, actualizó) el registro de incapacidad, licencia o certificado médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo de la fecha en la que se modifica la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Justificación, motivo o comentario que explica por qué se modificó la incapacidad, licencia o certificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo de Justificación de la modificación de incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4). Código del motivo de modificación de incapacidad. FK a HCMOANULB.CODMOTANU. Vincula profesional de salud con razón de cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "HCMOANULB" con la columna "TIPSERIPS", almacena el motivo por el cual se va a modificar la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20), PII ofuscado. Identificación del profesional de salud que realizó la modificación. FK a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationProfessionalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "INPROFSAL" con la columna "CODPROSAL", almacena el profesional que realizara la modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationProfessionalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha y hora en que se anuló, canceló o invalidó la incapacidad, licencia o certificado médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha que indica cuando se anula una incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Justificación o explicación del motivo por el cual se anuló la incapacidad, licencia o certificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación de la anulación de incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationObservation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4). Código del motivo de anulación de incapacidad. FK a HCMOANULB.CODMOTANU. Tipifica razón de cancelación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationReasonCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de anulación de la incapacidad. Hace referencia a la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationReasonCodeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationReasonCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20), PII ofuscado. Identificación del profesional de salud autorizado que anuló el registro. FK a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del profesional que anula la incapacidad referencia  a la tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Estado del registro: 1=Anulado (cancelado, invalidado), 2=Modificado (editado, actualizado).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Anulado
2 - Modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). JSON que almacena respuestas a cuestionario de licencias parentales flexibles del formulario de incapacidades y licencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FlexibleParentingResponses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda en JSON las respuestas a las licencias parentales flexibles del formulario de Incapacidades y licencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FlexibleParentingResponses';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FlexibleParentingResponses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Indica si hay embarazo múltiple en licencia derivada del proceso gestacional: 1=Sí, 2=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'MultiplePregnancyGestational';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo embarazo multiple del agrupador Licencia derivada del proceso gestacional
1 - Si
2 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'MultiplePregnancyGestational';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'MultiplePregnancyGestational';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(3,1). Edad gestacional (en semanas) del agrupador ''''Licencia derivada del proceso gestacional''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GestationalAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Edad gestacional del agrupador Licencia derivada del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GestationalAge';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GestationalAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(100). Nombre completo del padre, madre o cuidador del agrupador ''''Licencia para el cuidado de la niñez''''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NameOfParentOrCaregiver';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el campo Nombre del padre o cuidador del agrupador Licencia para el cuidado de la niñez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NameOfParentOrCaregiver';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NameOfParentOrCaregiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Presunto origen de la incapacidad: 1=Común (no laboral), 2=Laboral (accidente de trabajo, enfermedad profesional).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'PresumedOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presunto origen de la incapacidad: 1: Comun 2: Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'PresumedOrigin';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'PresumedOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Causa de retroactividad (antedatación): 1=Urgencia o internación del paciente, 2=Trastorno de memoria/confusión mental/desorientación, 3=Evento catastrófico o terrorista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CauseOfRetroactivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena la causa de retroactividad:   1 = Urgencias o internación del paciente  2 = Trastorno de memoria, confusión mental, desorientación en persona tiempo y lugar, otras alteraciones de la esfera psíquica, orgánica o funcional según criterio médico u odontólogo  3 = Evento catastrófico y terrorista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CauseOfRetroactivity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CauseOfRetroactivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3). Código de la especialidad médica u odontológica que emite la incapacidad. FK a INESPECIA.CODESPECI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si la incapacidad es retroactiva (antedatada): True=Sí, False=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'RetroactiveDisability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la incapacidad es retroactiva -- True = Sí , False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'RetroactiveDisability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'RetroactiveDisability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identificador del maestro de ''''Modalidades de atención'''' (consulta externa, internación, urgencia, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacena el id del maestro "Modalidades de atención"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Grupo de servicios donde se generó la incapacidad: 1=Consulta externa, 2=Apoyo diagnóstico/complementación, 3=Internación intrahospitalario, 4=Quirúrgico, 5=Atención inmediata/urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GroupServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo de servicios  1 - Consulta externa  2 - Apoyo diagnóstico clínico y complementación terapéutica  3 - Internación  (Intrahospitalario)  4 - Quirúrgico  5 - Atención inmediata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GroupServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GroupServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Clase o tipo de incapacidad/licencia: 1=Maternidad y paternidad, 2=Incapacidad origen común, 3=Licencia cuidado de niñez, 4=Otras licencias derivadas de proceso gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'DisabilityClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clase de incapacidad  
1. Licencia de maternidad y de paternidad (Certificado de licencia de maternidad y parentales)
2. Incapacidad de origen común (Certificado de incapacidad)
3. Certificado médico de la licencia para el cuidado de la niñez
4. Certificado de otras licencias derivadas del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'DisabilityClass';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'DisabilityClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NCHAR(10). Número del folio, comprobante o radicado en el cual se solicitó o registró la incapacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio en la cual se solicito la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(500). Código o descripción de la justificación cuando la incapacidad supera días máximos permitidos por ley.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVAJUSINCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de la incapacidad al superar el maximo permitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVAJUSINCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVAJUSINCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4). Código de justificación de incapacidad cuando excede límite máximo permitido. FK a HCMOANULB.CODMOTANU.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODJUSINCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la justificacion de la incapacidad al superar el maximo permitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODJUSINCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODJUSINCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(500). Observación, comentario o nota clínica adicional sobre la incapacidad, licencia o certificado médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10). Número único del ingreso o atención del paciente. FK a ADINGRESO.NUMINGRES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18). Consecutivo del registro anterior de incapacidad (si es prorroga o continuación). FK a HCINCAPAC.CODCONSEC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CONSECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Incapacidad Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CONSECANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CONSECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si es una prórroga, extensión o continuación de incapacidad anterior: True=Sí, False=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ESPRORROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es una Prorroga:  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ESPRORROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ESPRORROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha del accidente laboral que originó la incapacidad (si aplica para origen laboral).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECACCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Accidente Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECACCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECACCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de expedición, generación o registro oficial de la incapacidad, licencia o certificado médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Expedicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGIST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Tipo de ingreso: 1=Ambulatorio (consulta externa), 2=Hospitalario (internado, hospitalización).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'TIPOINCAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Ingreso:  1 = Ambulatorio   2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'TIPOINCAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'TIPOINCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Identifica la causa del ingreso/incapacidad: 1=Heridos combate, 2=Enfermedad profesional, 3=Enfermedad común adulto, 4=Enfermedad común pediatría, 5=Odontología, 6=Accidente tránsito, 7=Catastrofe, 8=Quemados, 9=Maternidad, 10=Accidente laboral, 11=Cirugía programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ICAUSAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ICAUSAING';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ICAUSAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4), ofuscado. OBSOLETO (PBI-6106, 21/10/2022). Código del diagnóstico CIE que justifica la incapacidad. Ya no se usa en refactoring.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'*Se deja obsoleta  por el refactoring de incpacidades PBI-6106  Codigo del Diagnostico - 21/10/2022 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Número total de días calendario de duración de la incapacidad, licencia o certificado médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMDIAINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Dias de Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMDIAINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NUMDIAINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha final, fecha de vencimiento o fecha de termino de la incapacidad, licencia o certificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECFININC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECFININC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECFININC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha inicial, fecha de inicio o fecha de vigencia desde la cual comienza la incapacidad, licencia o certificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECINIINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECINIINC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FECINIINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20), PII ofuscado. Identificación única del profesional de salud (médico, odontólogo) que emite la incapacidad. FK a INPROFSAL.CODPROSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10). Código de la unidad funcional, área, servicio o departamento donde se atiende al paciente. FK a INUNIFUNC.UFUCODIGO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10). Código del centro de atención, institución, clínica u hospital donde se genera la incapacidad. FK a ADCENATEN.CODCENATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25), PII ofuscado. Identificación única del paciente (cédula, documento, pasaporte). FK a INPACIENT.IPCODPACI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18). Consecutivo interno, clave primaria autoincrementada del registro de incapacidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incapacidades médicas emitidas a pacientes: registra cada certificado de incapacidad laboral con sus fechas, días otorgados, diagnóstico, causa, tipo, prórrogas y datos del profesional que la expide. Incluye información de anulaciones, modificaciones y modalidades de admisión relacionadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC';
