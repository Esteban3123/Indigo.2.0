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
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_CODJUSINCA];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_HCINCAPAC];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INESPECIA];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPACIENT];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_HCINCAPAC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCINCAPAC] NOCHECK CONSTRAINT [FK_Incapacidad_Ingreso];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo de la fecha en la que se modifica la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo de Justificación de la modificación de incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "HCMOANULB" con la columna "TIPSERIPS", almacena el motivo por el cual se va a modificar la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "INPROFSAL" con la columna "CODPROSAL", almacena el profesional que realizara la modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ModificationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha que indica cuando se anula una incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de la anulación de incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo de anulación de la incapacidad. Hace referencia a la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationReasonCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del profesional que anula la incapacidad referencia  a la tabla INPROFSAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Anulado
2 - Modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda en JSON las respuestas a las licencias parentales flexibles del formulario de Incapacidades y licencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'FlexibleParentingResponses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo embarazo multiple del agrupador Licencia derivada del proceso gestacional
1 - Si
2 - No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'MultiplePregnancyGestational';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Edad gestacional del agrupador Licencia derivada del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GestationalAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Nombre del padre o cuidador del agrupador Licencia para el cuidado de la niñez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'NameOfParentOrCaregiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presunto origen de la incapacidad: 1: Comun 2: Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'PresumedOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena la causa de retroactividad:   1 = Urgencias o internación del paciente  2 = Trastorno de memoria, confusión mental, desorientación en persona tiempo y lugar, otras alteraciones de la esfera psíquica, orgánica o funcional según criterio médico u odontólogo  3 = Evento catastrófico y terrorista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CauseOfRetroactivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la incapacidad es retroactiva -- True = Sí , False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'RetroactiveDisability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el id del maestro "Modalidades de atención"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de servicios  1 - Consulta externa  2 - Apoyo diagnóstico clínico y complementación terapéutica  3 - Internación  (Intrahospitalario)  4 - Quirúrgico  5 - Atención inmediata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'GroupServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de incapacidad  
1. Licencia de maternidad y de paternidad (Certificado de licencia de maternidad y parentales)
2. Incapacidad de origen común (Certificado de incapacidad)
3. Certificado médico de la licencia para el cuidado de la niñez
4. Certificado de otras licencias derivadas del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'DisabilityClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es una Prorroga:  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ESPRORROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ingreso:  1 = Ambulatorio   2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'TIPOINCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'ICAUSAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '*Se deja obsoleta  por el refactoring de incpacidades PBI-6106  Codigo del Diagnostico - 21/10/2022 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';

