/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * SERVICE CONTEXT MAPPER - Copyright 2013 (c) Paul Appeldoorn (DEVSHED.NL)
 * 
 * To contact me, please visit my blog at http://blog.devshed.nl/.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and 
 * associated documentation files (the "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 * copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the 
 * following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial 
 * portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT 
 * LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO 
 * EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
 * IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE 
 * USE OR OTHER DEALINGS IN THE SOFTWARE.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */
namespace DependencyContextMapper
{
    /// <summary> Provides the current context of the user session to the mapper. </summary>
    /// <remarks> Must be implemented and registered with SimpleInjector. </remarks>
    /// <example><![CDATA[ public class ContextProvider : IContextProvider<UserRole>
    /// {
    ///     public UserRole GeTContext()
    ///     {
    ///         object role = HttpContext.Current.Session["UserRole"];
    ///         if(role != null)
    ///         {
    ///             return (UserRole)role;
    ///         }
    ///         
    ///         return UserRole.Anonymous;
    ///     }
    /// }]]></example>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public interface IContextProvider<TContext>
    {
        /// <summary> Gets the current context for the dependency being resolverd. </summary>
        /// <returns></returns>
        TContext GetContext();
    }
}
