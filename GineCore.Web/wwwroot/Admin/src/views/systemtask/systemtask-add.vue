<template>
  <i-form :model="addSystemtaskInfo" ref="addsystemtaskForm" :label-width="100" :rules="rules">
    <Form-item label="任务名称" prop="title">
      <i-input v-model="addSystemtaskInfo.title" placeholder="请输入任务名称"></i-input>
    </Form-item>
    <Form-item label="选择专题库">
      <i-select v-model="addSystemtaskInfo.examClassId" style="width:200px">
        <i-option
          v-for="(item, index) in specialDataBaseList"
          :value="item.key"
          :key="index"
        >{{ item.value }}</i-option>
      </i-select>
    </Form-item>
    <Form-item label="运行时间">
      <Date-picker type="date" v-model="addSystemtaskInfo.runTimeDate" @on-change="changeDate" format="yyyy-MM-dd" placeholder="选择日期" style="width: 150px"></Date-picker>
      <Time-picker v-model="addSystemtaskInfo.runTimeTime" @on-change="changeTime" placeholder="请输入运行时间" style="width: 150px"></Time-picker>
    </Form-item>
    <Form-item label="任务类型类型">
      <i-select v-model="addSystemtaskInfo.taskCode" style="width:200px">
        <i-option v-for="(item, index) in taskTypes" :value="item.key" :key="index">{{ item.value }}</i-option>
      </i-select>
    </Form-item>
  </i-form>
</template>
<script>
export default {
  data() {
    return {
      addSystemtaskInfo: {
        title: "",
        examClassId: "",
        taskCode: 1,
        runTime: "",
        runTimeDate: '',
        runTimeTime: ''
      },
      date: '',
      time: '',
      specialDataBaseList: [
        { key: 1312, value: "三基专题库" },
        { key: 110, value: "出版社专题库" }
      ],
      rules: {
        title: [
          { required: true, message: "任务名称不能为空", trigger: "blur" },
          {
            type: "string",
            message: "任务名称不得少于2位",
            min: 2,
            trigger: "blur"
          }
        ]
      }
    };
  },

  props: {
    taskTypes: {
      type: Array,
      required: true
    }
  },

  created() {},

  methods: {
    clear() {
      this.addSystemtaskInfo.title = "";
      this.addSystemtaskInfo.examClassId = "";
      this.addSystemtaskInfo.runTime = '';
      this.addSystemtaskInfo.runTimeDate = '';
      this.addSystemtaskInfo.runTimeTime = '';
    },
    changeDate(date) {
      this.date = date;
      this.addSystemtaskInfo.runTime = this.date + " " + this.time;
    },

    changeTime(time){
      this.time = time;
      this.addSystemtaskInfo.runTime = this.date + " " + this.time;
    }
  }
};
</script>

