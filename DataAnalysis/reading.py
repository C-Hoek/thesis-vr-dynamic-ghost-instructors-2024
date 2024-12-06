import csv
from Vector3 import Vector3


def read_file(file_name):
    """
    This method reads a single trial file. It processes the errors, transparency values,
        ghost and student pointer positions, and the statistics.
    :param file_name: The name of the file that should be read.
    :return: A (error, transparency, ghost_pointer_pos, student_pointer_pos, statistics) tuple.
    """
    num = 0
    transparency = []
    error = []
    ghost_pointer_pos = []
    student_pointer_pos = []
    statistics = []

    with open(file_name, mode='r') as file:
        csvFile = csv.reader(file)

        for lines in csvFile:
            # Skip the trial information
            if num < 13:
                num = num + 1
                continue

            num = num + 1

            if lines[1] == "Error":
                error.append(read_error(lines))
            if lines[1] == "Ghost Transparency":
                transparency.append(read_transparency_line(lines))
            if lines[1] == "Ghost Hand Position":
                ghost_pointer_pos.append(read_ghost_pos(lines))
            if lines[1] == "Student Hand Position":
                student_pointer_pos.append(read_student_pos(lines))
            if lines[1] == "Statistics":
                statistics = read_statistics(lines)

    # # Check if there were any lines read in with a larger completion time and remove them if this is the case.
    # for type in [error, transparency, ghost_pointer_pos, student_pointer_pos]:
    #     counter = 0
    #     for line in type:
    #         if line[0] >= time_completed:
    #             counter += 1
    #     print(counter)

    return error, transparency, ghost_pointer_pos, student_pointer_pos, statistics


def read_transparency_line(line):
    """
    This method processes the ghost transparency lines.
    :param line: The array that contains the line information.
    :return: It returns a (time, transparency) tuple.
    """
    return float(line[0]), float(line[2])


def read_error(line):
    """
    This method processes the error line.
    :param line: The array that contains the line information.
    :return: It returns a (time, error) tuple.
    """
    return float(line[0]), float(line[2])


def read_ghost_pos(line):
    """
    This method processes the ghost position line.
    :param line: The array that contains the line information.
    :return: It returns a (time, position) tuple.
    """
    x, y, z = float(line[6][1:]), float(line[7]), float(line[8][:len(line[8]) - 1])
    # print(x, y, z)
    return float(line[0]), Vector3(x, y, z)


def read_student_pos(line):
    """
    This method processes the student position line.
    :param line: The array that contains the line information.
    :return: It returns a (time, position) tuple.
    """
    x, y, z = float(line[10][1:]), float(line[11]), float(line[12][:len(line[12]) - 1])
    # print(x, y, z)
    return float(line[0]), Vector3(x, y, z)


def read_statistics(line):
    """
    This method processes the statistics line.
    :param line: The array that contains the line information.
    :return: It returns a (time, average_error, max_error, min_error, time_to_completion, completed,
        task_performance, learning_effect) tuple.
    """
    average_error = float(line[2][28:])
    max_error = float(line[3][11:])
    min_error = float(line[4][11:])
    time_to_completion = float(line[5][19:])
    completed = True if line[6][-4:] == "True" else False
    task_performance = float(line[7][18:])
    learning_effect = float(line[8][17:len(line[8]) - 2])
    # print(average_error)
    # print(max_error)
    # print(min_error)
    # print(f'{time_to_completion}, {line[5][18:]}, {completed}, {line[6][-4:]}')
    # print(task_performance)
    # print(learning_effect)

    return (float(line[0]), average_error, max_error, min_error, time_to_completion,
            completed, task_performance, learning_effect)
