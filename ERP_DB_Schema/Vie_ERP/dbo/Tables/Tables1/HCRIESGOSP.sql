CREATE TABLE [dbo].[HCRIESGOSP] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRCES]    CHAR (15)                                                                        NOT NULL,
    [GESTACION]     TINYINT                                                                          NULL,
    [SIFGESCON]     TINYINT                                                                          NULL,
    [HIPERINDGES]   TINYINT                                                                          NULL,
    [HIPTERCON]     TINYINT                                                                          NULL,
    [SINTOMRESP]    TINYINT                                                                          NULL,
    [TUBERMULT]     TINYINT                                                                          NULL,
    [LEPRA]         TINYINT                                                                          NULL,
    [OBESDESNPRO]   TINYINT                                                                          NULL,
    [VICTMALTRATO]  TINYINT                                                                          NULL,
    [VICTVIOLSEX]   TINYINT                                                                          NULL,
    [INFTRASEX]     TINYINT                                                                          NULL,
    [ENFMENTAL]     TINYINT                                                                          NULL,
    [CANCERVIX]     TINYINT                                                                          NULL,
    [CANSENO]       TINYINT                                                                          NULL,
    [FECGESTA]      DATETIME                                                                         NULL,
    [FECSIFGES]     DATETIME                                                                         NULL,
    [FECHIPER]      DATETIME                                                                         NULL,
    [FECHIPOTE]     DATETIME                                                                         NULL,
    [FECSINTO]      DATETIME                                                                         NULL,
    [FECTUBER]      DATETIME                                                                         NULL,
    [FECLEPRA]      DATETIME                                                                         NULL,
    [FECOBEDES]     DATETIME                                                                         NULL,
    [FECVICMAL]     DATETIME                                                                         NULL,
    [FECVICVIOSEX]  DATETIME                                                                         NULL,
    [FECINFTRA]     DATETIME                                                                         NULL,
    [FECENFMEN]     DATETIME                                                                         NULL,
    [FECCANCER]     DATETIME                                                                         NULL,
    [FECCANSEN]     DATETIME                                                                         NULL,
    [LEISHMANIO]    TINYINT                                                                          NULL,
    [FECLEISHMA]    DATETIME                                                                         NULL,
    [VICTVIOLESEXU] INT                                                                              NULL,
    [ViolenceType]  TINYINT                                                                          NULL,
    [BurnedPerson]  BIT                                                                              NULL,
    [VICTCONFLI]    BIT                                                                              NULL,
    CONSTRAINT [PK_HCRIESGOSP] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRIESGOSP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCRIESGOSP] NOCHECK CONSTRAINT [FK_HCRIESGOSP_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCRIESGOSP__NUMINGRCES__INC__GESTACION]
    ON [dbo].[HCRIESGOSP]([NUMINGRCES] ASC)
    INCLUDE([GESTACION]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_HCRIESGOSP__IPCODPACI__NUMINGRCES]
    ON [dbo].[HCRIESGOSP]([IPCODPACI] ASC, [NUMINGRCES] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Victima de conflicto: True -> Si - False -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTCONFLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el paciente presenta quemaduras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'BurnedPerson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de violencia:  1 - Física  2 - Psicológica  3 - Neglicencia y abandono  4 - Sexual    (Debe estar llena cuando el campo VICTVIOLESEXU es 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ViolenceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Victima de violencia sexual: 1-> Si 2-> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLESEXU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro de Leishmaniosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEISHMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Leishmaniosis 1:si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEISHMANIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de cancer de seno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANSEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de cancer cervix', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECCANCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de enfermedades mentales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECENFMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de infecciones de transmisión sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECINFTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de violencia sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICVIOSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de victima de maltrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECVICMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de Obecidad o desnutricion proteico calorica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECOBEDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de lepra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECLEPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de tubercuosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECTUBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de sintomatico respiratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSINTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de Hipoteroidismo congenito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de Hipertencion inducida por la gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECHIPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de Sifilis Gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECSIFGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'FECGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cancer de seno:  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANSENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cancer de cérvix  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'CANCERVIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Enfermedad Mental  1-> si es diagnostico de ansiedad  2-> si es diagnostico de depresion  3-> si es diagnostico de esquizofrenia  4-> si es diagnostico de deficit de atencion por hiperactividad  5-> si es diagnostico de consumo de sustancias psicoactivas  6-> si es diagnostico de trastorno del ánimo bipolar  7-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ENFMENTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Infeccion de transmisión sexual  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'INFTRASEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Victima de violencia sexual  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTVIOLSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Victima de maltrato  0-> no aplica   1-> si es mujer victima de maltrato  2-> si es menor victima de maltrato  3-> no  21-> Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'VICTMALTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obesidad o desnutricion proteicocalorica  1-> si es obesidad  2-> si es desnutricion proteico calórica  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'OBESDESNPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lepra  1-> lepra paucibacilar  2-> lepra multibacilar  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'LEPRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tuberculosis Multidrogoresistente  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'TUBERMULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sintomático Respitatorio  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SINTOMRESP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipoteroidismo congénito  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPTERCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hipertencion inducida por la gestación  0-> no aplica  1-> si  2-> no  21->riesgo no evaluado    si este campo es = 1, el campo gestación debe ser tambien = 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'HIPERINDGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sifilis Gestacional:  0-> no Aplica  1->si es mujer con sifilis gestacional  2-> si es recien nacido con sifilis congenita  3-> no  21-> riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'SIFGESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la paciente esta en Gestación:   0-> no aplica  1-> si  2-> no  21-> Riesgo no evaluado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'NUMINGRCES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumérico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRIESGOSP', @level2type = N'COLUMN', @level2name = N'ID';

