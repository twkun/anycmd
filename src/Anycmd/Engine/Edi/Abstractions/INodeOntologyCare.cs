﻿
namespace Anycmd.Engine.Edi.Abstractions {
    using System;

    /// <summary>
    /// <remarks>
    /// 如此简单的模型为什么是接口？使用接口将其约束为不可变模型，从而使插件开发者不能使用正常手段修改它。
    /// </remarks>
    /// </summary>
    public interface INodeOntologyCare {
        /// <summary>
        /// 
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 
        /// </summary>
        Guid NodeId { get; }
        /// <summary>
        /// 
        /// </summary>
        Guid OntologyId { get; }

        DateTime? CreateOn { get; }
    }
}
