// -------------------------------------------------------------------------------------------------
// <copyright file="IHaveXmlNodeName.cs" company="Ninject Project Contributors">
//   Copyright (c) 2007-2009 Enkari, Ltd. All rights reserved.
//   Copyright (c) 2009-2017 Ninject Project Contributors. All rights reserved.
//
//   Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
//   You may not use this file except in compliance with one of the Licenses.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//   or
//       http://www.microsoft.com/opensource/licenses.mspx
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace Ninject.Extensions.Xml.Processors
{
    /// <summary>
    /// Specifies that the class has a xml node name.
    /// </summary>
    public interface IHaveXmlNodeName
    {
        /// <summary>
        /// Gets the name of the XML node.
        /// </summary>
        /// <value>The name of the XML node.</value>
        string XmlNodeName { get; }
    }
}