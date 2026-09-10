'*************************************************************
' Assembly         : Infraestructure.Data.Xpo.AuthorizationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/03/2020
'
' Copyright        : (c) . All rights reserved.
'*************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports DevExpress.Data.PLinq
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
#End Region

Public Class AuthorizationServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    ''' <summary>
    ''' Lista las autorizaciones de servicios tercerizados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAuthorizationOutsourcedServices() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationOutsourcedServicesXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationOutsourcedServicesXpo)), "Id;Code;DocumentDate;Status;StatusName;TypeName;Description;AdmissionInformation.CareGroupCodeName;AdmissionInformation.PatientCodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los cups que sean suceptibles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListCUPSSusceptibles(careCenterCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListCUPSSusceptiblesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareCenterCode = '" & careCenterCode & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListCUPSSusceptiblesXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    Dim CareCenterCode As String

    Public Function ListViewListCUPSSusceptibles2(_careCenterCode As String) As LinqInstantFeedbackSource
        Dim linqList As New LinqInstantFeedbackSource
        AddHandler linqList.GetQueryable, AddressOf linqList_GetQueryable
        linqList.KeyExpression = "Id"
        Me.CareCenterCode = _careCenterCode
        Return linqList
    End Function

    Private Sub linqList_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim table As New XPQuery(Of ViewListCUPSSusceptiblesXpo)(session)

            Dim TmpQueryableSource = From t In table
                                     Where t.CareCenterCode = Me.CareCenterCode
                                     Group t By CUPSEntityCode = t.CUPSEntityCode, CUPSEntityName = t.CUPSEntityName, CUPSEntityCodeName = t.CUPSEntityCodeName Into Group
                                     Select CUPSEntityCode, CUPSEntityName, CUPSEntityCodeName

            e.QueryableSource = TmpQueryableSource.AsQueryable()
            'e.Tag = tableCUPS
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Lista los productos que sean suceptibles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListProductsSusceptibles(careCenterCode As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListProductsSusceptiblesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CareCenterCode = '" & careCenterCode & "'")
        Dim _classEntity = session.GetClassInfo(GetType(ViewListProductsSusceptiblesXpo))
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

    ''' <summary>
    ''' Lista los usuarios que estan registrados en la plantilla de turnos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewListScheduleTemplateUsers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewListScheduleTemplateUsersXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewListScheduleTemplateUsersXpo)),
                                                          "UserId;UserCode;FullName;UserCodeName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los grupos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationScheduleTemplate() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationScheduleTemplateXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationScheduleTemplateXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los grupos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationScheduleTemplateByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {status}")
        Dim session As New IndigoXPOSession(Of AuthorizationScheduleTemplateXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationScheduleTemplateXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los grupos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationPortfolioXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationPortfolioXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los grupos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationPortfolioByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {status}")
        Dim session As New IndigoXPOSession(Of AuthorizationPortfolioXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationPortfolioXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los grupos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationGroup() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationGroupXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationGroupXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista los grupos de autorización por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationGroupByStatus(Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationGroupXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {Status}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationGroupXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de cancelación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCancellationReasons() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CancellationReasonsXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(CancellationReasonsXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName;ApplyWithdrawal", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de cancelación por usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListCancellationReasonsByUserCode(userCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 and CancellationReasonsUserXpo[UserCode = " & userCode & "]")
        Dim session As New IndigoXPOSession(Of CancellationReasonsXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(CancellationReasonsXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName;ApplyWithdrawal", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de postergación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPostponementReasons() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PostponementReasonsXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PostponementReasonsXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de cancelación por usuario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPostponementReasonsByUserCode(userCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 and PostponementReasonsUserXpo[UserCode = " & userCode & "]")
        Dim session As New IndigoXPOSession(Of PostponementReasonsXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PostponementReasonsXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los rechazos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationRejection() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationRejectionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationRejectionXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los rechazos de autorización
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationRejectionByUserCode(userCode As String) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 and AuthorizationRejectionUserXpo[UserCode = " & userCode & "]")
        Dim session As New IndigoXPOSession(Of AuthorizationRejectionXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationRejectionXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName", criteria)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de cancelación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationSource() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationSourceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationSourceXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName;Source", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos los motivos de cancelación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAuthorizationSourceByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AuthorizationSourceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status = {status}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(AuthorizationSourceXpo)),
                                                          "Id;Code;Name;Status;CodeName;StatusName;Source", criteria)
    End Function

    ''' <summary>
    ''' Lista todos las alertas asociadas a una solicitud por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListTraceabilityPaperworkAlertByStatus(traceabilityPaperworkId As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TraceabilityPaperworkAlertXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"TraceabilityPaperworkId = {traceabilityPaperworkId} AND Status = {status}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(TraceabilityPaperworkAlertXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing, Optional topRows As Integer? = Nothing, Optional withSort As Boolean = False, Optional propertyToOrderBy As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria, Nothing, withSort, topRows, propertyToOrderBy)
        result.Sort()
        Return result
    End Function

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO y unir dos listas enviándole la session de la primera, así no abra conflicto de session diferentes al unirlas
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionUnionXpo(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing, Optional session As Session = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria, session)
        result.Sort()
        Return result
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class