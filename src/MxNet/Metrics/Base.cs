﻿/*****************************************************************************
   Copyright 2018 The MxNet.Sharp Authors. All Rights Reserved.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
******************************************************************************/
using System;
using System.Reflection;

namespace MxNet.Metrics
{
    public class Base
    {
        public static EvalMetric Create(string metric, FuncArgs args)
        {
            var type = Assembly.GetExecutingAssembly().GetType("MxNet.Metric." + metric, true, true);
            return (EvalMetric) Activator.CreateInstance(type, args.Values);
        }

        public void CheckLabelShapes(NDArray labels, NDArray preds, bool shape = false)
        {
            Shape label_shape = null;
            Shape pred_shape = null;
            if (!shape)
            {
                label_shape = new Shape(labels.Size);
                pred_shape = new Shape(preds.Size);
            }
            else
            {
                label_shape = labels.Shape;
                pred_shape = preds.Shape;
            }

            if (shape)
            {
                if (labels.Shape[0] != preds.Shape[0])
                    throw new ArgumentException(string.Format(
                        "Shape of labels {0} does not match shape of predictions {1}", label_shape, pred_shape));
            }
            else
            {
                if (label_shape.Dimension != pred_shape.Dimension && label_shape.Size != pred_shape.Size)
                    throw new ArgumentException(string.Format(
                        "Shape of labels {0} does not match shape of predictions {1}", label_shape, pred_shape));
            }
        }
    }
}