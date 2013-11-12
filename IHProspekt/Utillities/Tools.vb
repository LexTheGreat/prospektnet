Namespace Utilities
    Public Module Tools
        Public Sub RemoveAt(Of T)(ByRef arr As T(), ByVal index As Integer)
            Dim uBound = arr.GetUpperBound(0)
            Dim lBound = arr.GetLowerBound(0)
            Dim arrLen = uBound - lBound

            If index < lBound OrElse index > uBound Then
                Throw New ArgumentOutOfRangeException( _
                String.Format("Index must be from {0} to {1}.", lBound, uBound))
            Else
                'create an array 1 element less than the input array
                Dim outArr(arrLen - 1) As T
                'copy the first part of the input array
                Array.Copy(arr, 0, outArr, 0, index)
                'then copy the second part of the input array
                Array.Copy(arr, index + 1, outArr, index, uBound - index)

                arr = outArr
            End If
        End Sub
    End Module
End Namespace
