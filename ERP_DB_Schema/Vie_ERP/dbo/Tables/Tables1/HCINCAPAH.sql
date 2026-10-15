CREATE TABLE [dbo].[HCINCAPAH] (
    [CONSECUTI]                    NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [IdHCINCAPAC]                  NUMERIC (18)   NOT NULL,
    [IPCODPACI]                    VARCHAR (25)   NOT NULL,
    [CODCENATE]                    CHAR (10)      NOT NULL,
    [UFUCODIGO]                    CHAR (10)      NOT NULL,
    [CODPROSAL]                    CHAR (20)      NOT NULL,
    [FECINIINC]                    DATETIME       NOT NULL,
    [FECFININC]                    DATETIME       NOT NULL,
    [NUMDIAINC]                    INT            NOT NULL,
    [CODDIAGNO]                    CHAR (4)       NULL,
    [ICAUSAING]                    INT            NOT NULL,
    [TIPOINCAP]                    INT            NULL,
    [FECREGIST]                    DATETIME       NOT NULL,
    [FECACCLAB]                    DATETIME       NULL,
    [ESPRORROG]                    BIT            NULL,
    [CONSECANT]                    NUMERIC (18)   NULL,
    [NUMINGRES]                    CHAR (10)      NULL,
    [OBSERVACI]                    CHAR (500)     NULL,
    [CODJUSINCA]                   CHAR (4)       NULL,
    [OBSERVAJUSINCA]               CHAR (500)     NULL,
    [NUMEFOLIO]                    NCHAR (10)     NULL,
    [DisabilityClass]              INT            NULL,
    [GroupServices]                INT            NULL,
    [IdAdmissionModalities]        INT            NULL,
    [RetroactiveDisability]        BIT            NULL,
    [CODESPECI]                    CHAR (3)       NULL,
    [CauseOfRetroactivity]         INT            NULL,
    [PresumedOrigin]               INT            NULL,
    [NameOfParentOrCaregiver]      VARCHAR (100)  NULL,
    [GestationalAge]               NUMERIC (3, 1) NULL,
    [MultiplePregnancyGestational] INT            NULL,
    [FlexibleParentingResponses]   VARCHAR (MAX)  NULL,
    [State]                        INT            NULL,
    [CancellationReasonCodeId]     CHAR (4)       NULL,
    [CancellationObservation]      VARCHAR (100)  NULL,
    [CancellationDate]             DATETIME       NULL,
    [ModificationObservation]      VARCHAR (100)  NULL,
    [ModificationProfessionalId]   CHAR (20)      NULL,
    [ModificationReasonId]         CHAR (4)       NULL,
    [ModificationDate]             DATETIME       NULL,
    [CancellationProfessionalId]   CHAR (20)      NULL,
    [IdIncapacidadPadre]           NUMERIC (18)   NULL,
    CONSTRAINT [PK_HCINCAPAH] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCINCAPAH_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCINCAPAH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINCAPAH_CancellationReasonCodeId] FOREIGN KEY ([CancellationReasonCodeId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCINCAPAH_CODJUSINCA] FOREIGN KEY ([CODJUSINCA]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCINCAPAH_IdModificationReason] FOREIGN KEY ([ModificationReasonId]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCINCAPAH_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCINCAPAH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCINCAPAH_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINCAPAH_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCINCAPAH_ModificationProfessionalId] FOREIGN KEY ([ModificationProfessionalId]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCINCAPAH_PROFESIONAL_ID] FOREIGN KEY ([CancellationProfessionalId]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_ADCENATEN];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_ADINGRESO];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_CODJUSINCA];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_INESPECIA];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_INPACIENT];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_INPROFSAL];


GO
ALTER TABLE [dbo].[HCINCAPAH] NOCHECK CONSTRAINT [FK_HCINCAPAH_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "INPROFSAL" con la columna "CODPROSAL", almacena el profesional que realizara la cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CancellationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo de la fecha en la que se modifica la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "HCMOANULB" con la columna "TIPSERIPS", almacena el motivo por el cual se va a modificar la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ModificationReasonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "INPROFSAL" con la columna "CODPROSAL", almacena el profesional que realizara la modificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ModificationProfessionalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo de Justificación de la modificación de incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ModificationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cancelacion de la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CancellationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observacion de la cancelacion de la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CancellationObservation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Columna de "HCINCAPAC" relacionada con la tabla "HCMOANULB" con la columna "TIPSERIPS", almacena el motivo por el cual se va a cancelar la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CancellationReasonCodeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Anulado
2 - Modificado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda en JSON las respuestas a las licencias parentales flexibles del formulario de Incapacidades y licencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'FlexibleParentingResponses';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Edad gestacional del agrupador Licencia derivada del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'GestationalAge';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el campo Nombre del padre o cuidador del agrupador Licencia para el cuidado de la niñez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'NameOfParentOrCaregiver';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presunto origen de la incapacidad: 1: Comun 2: Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'PresumedOrigin';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la causa de retroactividad:   1 = Urgencias o internación del paciente  2 = Trastorno de memoria, confusión mental, desorientación en persona tiempo y lugar, otras alteraciones de la esfera psíquica, orgánica o funcional según criterio médico u odontólogo  3 = Evento catastrófico y terrorista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CauseOfRetroactivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la incapacidad es retroactiva -- True = Sí , False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'RetroactiveDisability';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el id del maestro "Modalidades de atención"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de servicios  1 - Consulta externa  2 - Apoyo diagnóstico clínico y complementación terapéutica  3 - Internación  (Intrahospitalario)  4 - Quirúrgico  5 - Atención inmediata', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'GroupServices';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clase de incapacidad  
1. Licencia de maternidad y de paternidad (Certificado de licencia de maternidad y parentales)
2. Incapacidad de origen común (Certificado de incapacidad)
3. Certificado médico de la licencia para el cuidado de la niñez
4. Certificado de otras licencias derivadas del proceso gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'DisabilityClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero del Folio en la cual se solicito la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la justificacion de la incapacidad al superar el maximo permitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'OBSERVAJUSINCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la justificacion de la incapacidad al superar el maximo permitido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CODJUSINCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación de la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la Incapacidad Anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CONSECANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Es una Prorroga:  True = Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ESPRORROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del Accidente Laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'FECACCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Expedicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Ingreso:  1 = Ambulatorio   2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'TIPOINCAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica la Causa del Ingreso:  1. Heridos en combate   2. Enfermedad profesional   3. Enfermedad general adulto   4. Enfermedad general pediatria   5. Odontología   6. Accidente de transito   7. Catastrofe/Fisalud   8. Quemados   9. Maternidad  10. Accidente Laboral  11. Cirugia Programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'ICAUSAING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'*Se deja obsoleta  por el refactoring de incpacidades PBI-6106  Codigo del Diagnostico - 21/10/2022 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de Dias de Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'NUMDIAINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Final de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'FECFININC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha Inicial de la Incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'FECINIINC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la incapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'IdHCINCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINCAPAH', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

