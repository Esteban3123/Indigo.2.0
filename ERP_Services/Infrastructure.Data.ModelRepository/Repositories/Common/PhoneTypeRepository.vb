'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class PhoneTypeRepository
    Inherits GenericRepository(Of PhoneType)
    Implements IPhoneTypeRepository


    'Contexto de commmon
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un tipo de telefono especifico
    ''' </summary>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    Public Function GetPhoneType(ByVal code As String) As PhoneType Implements IPhoneTypeRepository.GetPhoneType
        Dim phoneType = From e In _context.PhoneType
                        Where e.Code = code
                        Select e
        If phoneType.Count > 0 Then
            Return phoneType.Single()
        Else
            Return New PhoneType()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los tipos de telefonos
    ''' </summary>
    ''' <returns>Lista de tipo de telefono</returns>
    ''' <remarks></remarks>
    Public Function ListAllPhoneType() As List(Of PhoneType) Implements IPhoneTypeRepository.ListAllPhoneType
        Dim phoneType = From e In _context.PhoneType
                        Select e
        Return phoneType.ToList()
    End Function
    ''' <summary>
    ''' fubncion para obtener el tupo de telefono por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="desatach"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPhonetypeByCode(code As String, Optional desatach As Boolean = True) As PhoneType Implements IPhoneTypeRepository.GetPhonetypeByCode
        Dim phonetype = From e In _context.PhoneType
                 Where e.Code = code
                 Select e
        If phonetype.Count > 0 Then
            Dim objPhoneType = Nothing
            If desatach = False Then
                objPhoneType = (From e In _context.PhoneType.AsNoTracking
                                 Where e.Code = code
                                 Select e).SingleOrDefault
            Else
                objPhoneType = phonetype.SingleOrDefault()
            End If
            Return objPhoneType
        Else
            Return New PhoneType
        End If
    End Function
End Class
