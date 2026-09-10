#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()>
Public Interface IAgreementsRedemptionPoints

    ''' <summary>
    ''' Función que obtiene todos los convenios de redencion de puntos
    ''' </summary>
    ''' <returns>Lista de Fabricantess</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllAgreementsRedemptionPoints(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función que obtiene por codigo los convenios de redencion de puntos
    ''' </summary>
    ''' <param name="Code">Código de la Fabricantes</param>
    ''' <returns>AgreementsRedemptionPoints</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAgreementsRedemptionPointsByCode(Code As String, session As SessionValues) As AgreementsRedemptionPoints

    ''' <summary>
    ''' Función para Almacenar los convenios de redencion de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAgreementsRedemptionPoints(AgreementsRedemptionPoints As Domain.Entities.AgreementsRedemptionPoints, idSequense As Int64, audit As AuditMessage) As ActionResult(Of AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función para Eliminar los convenios de redencion de puntos
    ''' </summary>
    ''' <param name="AgreementsRedemptionPoints">Objeto AgreementsRedemptionPoints</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteAgreementsRedemptionPoints(AgreementsRedemptionPoints As Domain.Entities.AgreementsRedemptionPoints, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>    
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeAgreementsRedemptionPointsStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of AgreementsRedemptionPoints)


    ''' <summary>
    ''' Obtiene los detalles de un convenio de redencion de puntos
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail)


End Interface
